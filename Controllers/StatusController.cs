using Bme.Swlab1.Rest.Dtos;
using Bme.Swlab1.Rest.Services;

using Microsoft.AspNetCore.Mvc;

namespace Bme.Swlab1.Rest.Controllers;

// TODO: REPLACE neptun WITH YOUR NEPTUN CODE SMALL CAPS
// TODO: CSERELD LE A neptun-t a SAJAT NEPTUN KODODRA KISBETUKKEL
[Route("api/[controller]/hhuz6k")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    // DO NOT CHANGE THE CONSTRUCTOR!
    // NE VALTOZTSD MEG A KONSTRUKTORT!
    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet]
    public IEnumerable<Status> List()
    {
        return _statusService.List();
    }

    [HttpHead("{statusName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult ExistsWithName(string statusName)
    {
        return _statusService.ExistsWithName(statusName) ? Ok() : NotFound();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Status> Get(int id)
    {
        var value = _statusService.FindById(id);
        return value != null ? Ok(value) : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Status> Create([FromBody] CreateStatus value)
    {
        try
        {
            var created = _statusService.Insert(value);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError(nameof(CreateStatus.Name), ex.Message);
            return ValidationProblem(ModelState);
        }
    }
}
