using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskScheduler.Core.Exceptions;
using TaskScheduler.Core.Interfaces.Base;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;
using TaskScheduler.Manager.Interfaces;
using TaskScheduler.Provider.Implementation;

namespace TaskScheduler.Manager.Managers
{
    public class UserManager : CrudManager<IRepository<User>,User,UserCreateUpdateModel,UserDto>,
        IUserManager
    {
        public UserManager(ICommitProvider commitProvider, IMapper mapper, IRepository<User> userRepository)
            : base(userRepository, mapper, commitProvider)
        {
        }

    }
}
