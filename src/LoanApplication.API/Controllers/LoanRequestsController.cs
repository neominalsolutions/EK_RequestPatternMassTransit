using LoanApplication.API.Dtos;
using MassTransit;
using MassTransit.Clients;
using Messaging.CustomerCredit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanApplication.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LoanRequestsController : ControllerBase
  {
    private readonly IRequestClient<GetCreditScoreRequest> client;
    public LoanRequestsController(IRequestClient<GetCreditScoreRequest> client)
    {
      this.client = client;
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
  }
}
