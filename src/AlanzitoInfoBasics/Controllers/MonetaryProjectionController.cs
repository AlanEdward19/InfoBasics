using Application.Queries.MonetaryProjection;
using Microsoft.AspNetCore.Mvc;

namespace AlanzitoInfoBasics.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonetaryProjectionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromServices] MonetaryProjectionQueryHandler queryHandler, 
            [FromQuery] MonetaryProjectionQuery query)
        {
            return Ok(await queryHandler.Handle(query));
        }
    }
}
