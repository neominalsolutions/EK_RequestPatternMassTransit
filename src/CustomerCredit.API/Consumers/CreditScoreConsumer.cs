using MassTransit;
using Messaging.CustomerCredit;

namespace CustomerCredit.API.Consumers
{
  // Not: API endpoint tetiklemek yerine queuee üzeirnden bir servis tetikleme yönetimi ile yaklaşıyoruz.
  public class CreditScoreConsumer : IConsumer<GetCreditScoreRequest>
  {
    public async Task Consume(ConsumeContext<GetCreditScoreRequest> context)
    {
      if(context.Message.requestAmount > 150000)
      {
        // response dönüşü yaptık
        await context.RespondAsync(new CreditNotSuitableResponse("Maksimum 100,000 e kadar kredi çekebilirsiniz"));
      }
      else
      {
        // bu account burada kişinin hesap bilgisine göre kredi notu hesaplama algoritması olması lazım. ona göre sonuç ve ödenebilecek maks miktar verilecektir. 
        await context.RespondAsync(new CreditIsSuitableResponse(80000, creditScore: 150));
      }
    }
  }
}
