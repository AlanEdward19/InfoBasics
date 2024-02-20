using Application.Commands.FinancialInvestment;
using Application.Queries.Expense;
using Application.Queries.FinancialInvestment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlanzitoInfoBasics.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FinancialInvestmentController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] FinancialInvestmentQueryHandler queryHandler,
            [FromQuery] FinancialInvestmentQuery query)
        {
            return Ok(await queryHandler.Handle(query));
        }

        [HttpPut("BuySellShares")]
        public async Task<IActionResult> BuySellShares([FromServices] FinancialInvestmentCommandHandler commandHandler,
            [FromQuery] Guid id, [FromBody] BuySellSharesCommand command)
        {
            return Created(HttpContext.Request.Path, await commandHandler.BuySellShares(id, command));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] FinancialInvestmentCommandHandler commandHandler,
            [FromBody] CreateFinancialInvestmentCommand command)
        {
            return Created(HttpContext.Request.Path, await commandHandler.Insert(command));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromServices] FinancialInvestmentCommandHandler commandHandler,
            [FromQuery] Guid id, [FromBody] UpdateFinancialInvestmentCommand command)
        {
            return Created(HttpContext.Request.Path, commandHandler.Update(id, command));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromServices] FinancialInvestmentCommandHandler commandHandler,
            [FromQuery] Guid id, [FromBody] UpdatePartiallyFinancialInvestmentCommand command)
        {
            return Ok(await commandHandler.Patch(id, command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromServices] FinancialInvestmentCommandHandler commandHandler,
            [FromQuery] Guid id)
        {
            if (await commandHandler.Delete(id))
                return NoContent();

            return BadRequest("A Error occurred trying to delete entity");
        }
    }
}
