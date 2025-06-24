using Microsoft.AspNetCore.Mvc;
using Unistream.Application.BusinessLogic.Balances.Queries.GetByClientId;

namespace Unistream.Api.Controllers;

[ApiController]
[Route("balance")]
public sealed class BalanceController : ApiController
{
    [HttpGet]
    public async Task<BalanceResponse> GetBalanceByClientId(
        [FromQuery] Guid id
    )
    {
        return await Sender.Send(new GetBalanceByClientIdQuery(id));
    }
}
