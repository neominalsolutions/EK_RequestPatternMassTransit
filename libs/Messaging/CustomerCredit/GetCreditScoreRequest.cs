using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.CustomerCredit
{
  // şu hesap için şu kadarlık bir miktar kredi çekmek için onay var mı
  public record GetCreditScoreRequest(string accountNumber,decimal requestAmount);
 
}
