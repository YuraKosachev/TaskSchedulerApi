using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Response;
using TaskScheduler.Manager.Interfaces;
using TaskScheduler.Manager.Managers;

namespace TaskScheduler.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get users
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IList<UserDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(WorkTaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("user/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var task = await _userManager.GetAsync((m) => m.Id == id, "Tasks");
            return Ok(task);
        }

        /// <summary>
        /// create user
        /// </summary>
        /// <param name="iata">airport iata code</param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateUpdateModel model,
            [FromServices] IValidator<UserCreateUpdateModel> validator)
        {
            validator.ValidateAndThrow(model);
            await _userManager.CreateAsync(model);
            return Ok();
        }


        /// <summary>
        /// update user
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("user/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id,
            [FromBody] UserCreateUpdateModel model,
            [FromServices] IValidator<UserCreateUpdateModel> validator)
        {
            validator.ValidateAndThrow(model);
            await _userManager.UpdateAsync(id, model);
            return Ok();
        }

        /// <summary>
        /// delete user
        /// </summary>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("user/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userManager.RemoveAsync(id);
            return Ok();
        }
    }
}
