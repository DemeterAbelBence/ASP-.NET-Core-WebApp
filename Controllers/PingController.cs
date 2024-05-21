using Microsoft.AspNetCore.Mvc;

namespace Bme.Swlab1.Rest.Controllers;

// DO NOT CHANGE ANYTHING!
// NE VALTOZTASS MEG SEMMIT!
[Route("api/[Controller]")]
[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Ping()
    {
        return Ok("pong");
    }
}
