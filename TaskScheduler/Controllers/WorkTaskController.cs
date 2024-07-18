using FluentValidation;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskScheduler.Core.Enums;
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
    public class WorkTaskController : ControllerBase
    {
        private readonly IWorkTaskManager _workTaskManager;
        public WorkTaskController(IWorkTaskManager workTaskManager)
        {
            _workTaskManager = workTaskManager;
        }

        /// <summary>
        /// Get tasks
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IList<WorkTaskDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("work_tasks")]
        public async Task<IActionResult> GetTasks(WorkTaskStatus? status = null, Guid? priorityId = null)
        {
            var predicate = PredicateBuilder.New<WorkTask>(true);

            if (status.HasValue) {
                predicate = predicate.And(m => m.Status == status);
            }
            if (priorityId.HasValue) 
            {
                predicate = predicate.And(m => m.PriorityId == priorityId);
            }
            var tasks = await _workTaskManager.GetAllAsync(predicate);
            return Ok(tasks);
        }

        /// <summary>
        /// Get task
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(WorkTaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("work_task/{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _workTaskManager.GetAsync((m) => m.Id == id, "User", "Priority");
            return Ok(task);
        }

        /// <summary>
        /// create task
        /// </summary>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("work_task")]
        public async Task<IActionResult> CreateTask([FromBody] WorkTaskCreateUpdateModel model,
            [FromServices] IValidator<WorkTaskCreateUpdateModel> validator)
        {
            validator.ValidateAndThrow(model);
            await _workTaskManager.CreateAsync(model);
            return Ok();
        }

        /// <summary>
        /// create task
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(WorkTaskDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("work_task/assigment")]
        public async Task<IActionResult> TaskAssignment([FromBody] AssigmentWorkTaskModel model)
        {
            var result = await _workTaskManager.AssigmentTo(model);
            return Ok(result);
        }


        /// <summary>
        /// update task
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("work_task/{id}")]
        public async Task<IActionResult> UpdateTask(Guid id,
            [FromBody] WorkTaskCreateUpdateModel model,
            [FromServices] IValidator<WorkTaskCreateUpdateModel> validator)
        {
            validator.ValidateAndThrow(model);
            await _workTaskManager.UpdateAsync(id, model);
            return Ok();
        }

        /// <summary>
        /// delete task
        /// </summary>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorList), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(500)]
        [Route("work_task/{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _workTaskManager.RemoveAsync(id);
            return Ok();
        }
    }
}
