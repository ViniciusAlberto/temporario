using Maverick.Domain.Models;
using System.Threading.Tasks;

namespace Maverick.Domain.Adapters
{
    public interface ILogDbAdapter
    {
        /// <summary>
        /// Salva dados de log
        /// </summary>
        /// <param name="logContrato">Objeto com dados do log</param> 
        Task SalvarLogAsync(LogContrato logContrato);
    }
}
