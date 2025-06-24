using Microsoft.AspNetCore.Mvc;
using Unistream.Application.BusinessLogic.Transactions.Commands.Credit;
using Unistream.Application.BusinessLogic.Transactions.Commands.Debit;
using Unistream.Application.BusinessLogic.Transactions.Commands.Revert;

namespace Unistream.Api.Controllers;

[ApiController]
[Route("")]
public sealed class TransactionController : ApiController
{
    [HttpPost("credit")]
    public async Task<CreditTransactionResponse> CreditTransaction(
        [FromBody] CreditTransactionRequest request
    )
    {
        return await Sender.Send(
            new CreditTransactionCommand(
                request.Id,
                request.ClientId,
                request.DateTime,
                request.Amount
            )
        );
    }

    [HttpPost("debit")]
    public async Task<DebitTransactionResponse> DebitTransaction(
        [FromBody] DebitTransactionRequest request
    )
    {
        return await Sender.Send(
            new DebitTransactionCommand(
                request.Id,
                request.ClientId,
                request.DateTime,
                request.Amount
            )
        );
    }

    [HttpPost("revert")]
    public async Task<RevertTransactionResponse> RevertTransaction(
        [FromQuery] Guid id
    )
    {
        return await Sender.Send(
            new RevertTransactionCommand(
                id
            )
        );
    }
}
