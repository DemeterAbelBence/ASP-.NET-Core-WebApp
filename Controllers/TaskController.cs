using Bme.Swlab1.Rest.Dtos;
using Bme.Swlab1.Rest.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bme.Swlab1.Rest.Controllers;

[Route("api/[controller]/hhuz6k")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IEnumerable<Dtos.Task> List()
    {
        return _taskService.List();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Status> Get(int id)
    {
        var value = _taskService.FindById(id);
        return value != null ? Ok(value) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Dtos.Task> Create([FromBody] CreateTask value)
    {
        try
        {
            var created = _taskService.Insert(value);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(nameof(CreateTask.Title), ex.Message);
            return ValidationProblem(ModelState);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Dtos.Task> Delete(int id)
    {
        var value = _taskService.Delete(id);
        return value != null ? Ok(value) : NotFound();
    }

    [HttpPatch("{id}/done")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Dtos.Task> MarkDone(int id)
    {
        var value = _taskService.MarkDone(id);
        return value != null ? Ok(value) : NotFound();
    }

    [HttpPatch("{id}/move")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Dtos.Task> MoveToStatus(int id, [FromBody] MoveToStatus value)
    {
        var task = _taskService.MoveToStatus(id, value.status);
        return task != null ? Ok(task) : NotFound();
    }
}

