using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.CustomerCredit
{
  // Onay verilen miktar ve kredi Skor dönsün.
  public record CreditIsSuitableResponse(decimal appliedAmount, int creditScore); // 150 
  
}
