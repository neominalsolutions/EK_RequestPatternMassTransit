namespace LoanApplication.API.Dtos
{
  public record LoanRequestDto(string firstName,string accountNumber,decimal requestAmount, decimal annualIncome);
 
}
