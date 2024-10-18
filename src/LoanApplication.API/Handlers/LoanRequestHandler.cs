using LoanApplication.API.Dtos;
using MassTransit;
using MediatR;
using Messaging.CustomerCredit;

namespace LoanApplication.API.Handlers
{
  public class LoanRequestHandler : IRequestHandler<LoanRequestDto>
  {
    private readonly IRequestClient<GetCreditScoreRequest> client;

    public LoanRequestHandler(IRequestClient<GetCreditScoreRequest> client)
    {
      this.client = client;
    }

    // request handler gelen request işleyecek olan use case servismizi.
    public async Task Handle(LoanRequestDto request, CancellationToken cancellationToken)
    {

      var creditScoreReq = new GetCreditScoreRequest(request.accountNumber, request.requestAmount);

      var response = await this.client.GetResponse<CreditIsSuitableResponse, CreditNotSuitableResponse>(creditScoreReq);


       await Task.CompletedTask;
    }
  }
}
