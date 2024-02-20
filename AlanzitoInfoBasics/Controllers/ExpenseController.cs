using Application.Commands.Expense;
using Application.Queries.Expense;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlanzitoInfoBasics.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get([FromServices] ExpenseQueryHandler queryHandler, [FromQuery] ExpenseQuery query)
        {
            return Ok(await queryHandler.Handle(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromServices] ExpenseCommandHandler commandHandler, [FromQuery] CreateExpenseCommand command)
        {
            return Ok(await commandHandler.Insert(command));
        }

        [HttpPut("PayOffInstallment")]
        public async Task<IActionResult> PayOffInstallment([FromServices] ExpenseCommandHandler commandHandler, [FromQuery]Guid installmentId)
        {
            var (success, result) = await commandHandler.PayOffInstallment(installmentId);
            
            if(!success)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromServices] ExpenseCommandHandler commandHandler, [FromQuery] Guid id,[FromBody] UpdateExpenseCommand command)
        {
            return Ok(await commandHandler.Update(id, command));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromServices] ExpenseCommandHandler commandHandler,
            [FromQuery] Guid id, [FromBody] UpdatePartiallyExpenseCommand command)
        {
            return Ok(await commandHandler.Patch(id, command));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromServices] ExpenseCommandHandler commandHandler, [FromQuery] Guid id)
        {
            return Ok(await commandHandler.Delete(id));
        }
    }
}
