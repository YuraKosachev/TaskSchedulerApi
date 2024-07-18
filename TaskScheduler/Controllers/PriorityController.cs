using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskScheduler.Core.Models.CreateUpdate;
using TaskScheduler.Core.Models.Dto;
using TaskScheduler.Core.Models.Entities;
using TaskScheduler.Core.Models.Response;
using TaskScheduler.Manager.Interfaces;

namespace TaskScheduler.Controllers
{
    [Route("api")]
    [ApiController]
    [Produces("application/json")]
    public class PriorityController : ControllerBase
    {
        private readonly IPriorityManager _priorityManager;
        public PriorityController(IPriorityManager priorityManager)
        {
            _priorityManager = priorityManager;
        }

        /// <summary>
        /// Get priorities
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IList<PriorityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("priorities")]
        public async Task<IActionResult> GetPriorities()
        {
            var priorities = await _priorityManager.GetAllAsync();
            return Ok(priorities);
        }

        /// <summary>
        /// Get priority
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PriorityDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("priority/{id}")]
        public async Task<IActionResult> GetPriority(Guid id)
        {
            var priority = await _priorityManager.GetAsync((m) => m.Id == id);
            return Ok(priority);
        }

        /// <summary>
        /// create priority
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("priority")]
        public async Task<IActionResult> CreatePriority([FromBody] PriorityCreateUpdateModel model,
            [FromServices] IValidator<PriorityCreateUpdateModel> validator)
        {
            validator.ValidateAndThrow(model);
            await _priorityManager.CreateAsync(model);
            return Ok();
        }


        /// <summary>
        /// update priority
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("priority/{id}")]
        public async Task<IActionResult> UpdatePriority(Guid id, 
            [FromBody] PriorityCreateUpdateModel model,
            [FromServices] IValidator<PriorityCreateUpdateModel> validator)
        {
            validator.ValidateAndThrow(model);

            await _priorityManager.UpdateAsync(id, model);
            return Ok();
        }

        /// <summary>
        /// delete priority
        /// </summary>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("priority/{id}")]
        public async Task<IActionResult> DeletePriority(Guid id)
        {
            await _priorityManager.RemoveAsync(id);
            return Ok();
        }
    }
}
