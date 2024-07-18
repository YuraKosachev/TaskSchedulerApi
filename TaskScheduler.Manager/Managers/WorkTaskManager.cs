using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskScheduler.Core.Exceptions;
using TaskScheduler.Core.Interfaces.Base;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;
using TaskScheduler.Manager.Interfaces;

namespace TaskScheduler.Manager.Managers
{
    public class WorkTaskManager : CrudManager<IRepository<WorkTask>, WorkTask, WorkTaskCreateUpdateModel, WorkTaskDto>,
        IWorkTaskManager
    {
        public WorkTaskManager(IRepository<WorkTask> repository, IMapper mapper, ICommitProvider commitProvider)
            : base(repository, mapper, commitProvider)
        {
        }

        public async Task<WorkTaskDto> AssigmentTo(AssigmentWorkTaskModel model)
        {
            var task = await _repository.FindAsync(model.TaskId);
            if (task == null)
            {
                throw new ItemNotFoundException("task exception : item not found");
            }

            _repository.Update(_mapper.Map(model, task));

            await _commitProvider.SaveChangesAsync();

            var entity = await _repository.AllAsNoTracking
                .Include(x => x.User)
                .Include(x => x.Priority)
                .FirstOrDefaultAsync(x => x.Id == model.TaskId);

            return _mapper.Map<WorkTaskDto>(entity);
        }
    }
}
