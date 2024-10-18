using LoanApplication.API.Dtos;
using MassTransit;
using MediatR;
using Messaging.CustomerCredit;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplication.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoanRequestsController : ControllerBase
  {
    private readonly IRequestClient<GetCreditScoreRequest> client;
    private readonly IMediator mediator; // Kule tüm request sürecini yönetecek.
    public LoanRequestsController(IRequestClient<GetCreditScoreRequest> client, IMediator mediator)
    {
      this.client = client;
      this.mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> GetLoanRequest([FromBody] LoanRequestDto request)
    {
      var creditScoreReq = new GetCreditScoreRequest(request.accountNumber, request.requestAmount);

      var response = await this.client.GetResponse<CreditIsSuitableResponse, CreditNotSuitableResponse>(creditScoreReq);

      //return Ok(response.Message);

      if(response.Is(out Response<CreditIsSuitableResponse> result))
      {
        return Ok(result.Message);
      }
      else if(response.Is(out Response<CreditNotSuitableResponse> result2)) 
      {
        return BadRequest(result2.Message);
      }

      return Ok(response.Message);
    }


    [HttpPost("v2")]
    public async Task<IActionResult> GetLoanRequestWithMediatR([FromBody] LoanRequestDto request)
    {
      // Masstransit de commandler Send methodu ile gönderiliyordu. Eventlerde Publish ile gönderiliyordu.
      // Message Broker üzerinden çalışan

      // Mediator=> sadece API içindeki servislerin event driven geliştirilmesi için bir teknik. In Memory
      // Meditor içinde RequestHandlerlar command görevi görür. Send ile çağırılır
      // Eğer bir event varsa INotification interface  ile bu süreci yöneteceğiz.
      // Bu tarz durumlarda ise Publish methodunu kullanıcağız.

      await this.mediator.Send(request);

      return Ok();
    }
  }
}
