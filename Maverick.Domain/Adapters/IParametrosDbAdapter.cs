using Maverick.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maverick.Domain.Adapters
{
    public interface IParametrosDbAdapter
    {
        /// <summary>
        /// Realiza a busca o template da mensagem do SMS
        /// </summary>   
        /// <returns>
        ///    Mensagem template para envio do SMS
        /// </returns>
        Task<string> BuscarMensagemSMSAsync();
    }
}
