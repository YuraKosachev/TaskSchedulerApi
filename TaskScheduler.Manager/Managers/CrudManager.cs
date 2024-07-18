using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TaskScheduler.Core.Exceptions;
using TaskScheduler.Core.Interfaces.Base;
using TaskScheduler.Core.Models;
using TaskScheduler.Core.Models.Dto;

namespace TaskScheduler.Manager.Managers
{
    public abstract class CrudManager<TIRepository, TEntity, TCreateUpdateModel, TDto> : CommitManager
        where TIRepository : IRepository<TEntity>
        where TEntity : HasDbIdentityId
        where TCreateUpdateModel : class
        where TDto : class
    {
        protected readonly TIRepository _repository;
        protected readonly IMapper _mapper;
        protected CrudManager(TIRepository repository, IMapper mapper, ICommitProvider commitProvider) : base(commitProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateAsync(TCreateUpdateModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity);
            await _commitProvider.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(ICollection<TCreateUpdateModel> models)
        {
            var entities = models.Select(m => _mapper.Map<TEntity>(m)).ToList();
            await _repository.AddRangeAsync(entities);
            await _commitProvider.SaveChangesAsync();
        }

        public async Task<IList<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes)
        {
            if (predicate == null)
            {
                predicate = (e) => true;
            }

            var asNonTrackingRepo = _repository.AllAsNoTracking;

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    asNonTrackingRepo = asNonTrackingRepo.Include(include);
                }

            }
            var list = await asNonTrackingRepo
                .Where(predicate)
                .Select(x => _mapper.Map<TDto>(x))
                .ToListAsync();

            return list;
        }

        public async Task<TDto> GetAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes)
        {
            if (predicate == null)
            {
                predicate = (e) => true;
            }

            var asNonTrackingRepo = _repository.AllAsNoTracking;

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    asNonTrackingRepo = asNonTrackingRepo.Include(include);
                }

            }

            var item = await asNonTrackingRepo.FirstOrDefaultAsync(predicate);

            return item == null ? null : _mapper.Map<TDto>(item);
        }

        public async Task UpdateAsync(Guid id, TCreateUpdateModel model)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
            {
                throw new ItemNotFoundException("update item exception : item not found");
            }
            _repository.Update(_mapper.Map(model, entity));

            await _commitProvider.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {

            var entity = await _repository.FindAsync(id);
            if (entity == null)
            {
                throw new ItemNotFoundException("remove item exception : item not found");
            }
            _repository.Remove(entity);
            await _commitProvider.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(Guid[] ids)
        {

            var entities = await _repository.All.Where(x => ids.Contains(x.Id)).ToArrayAsync();
            if (entities?.Any() != true)
            {
                throw new ItemNotFoundException("remove items exception : items not found");
            }
            _repository.RemoveRange(entities);
            await _commitProvider.SaveChangesAsync();
        }

    }
}
