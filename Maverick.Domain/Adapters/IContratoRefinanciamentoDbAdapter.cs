using Maverick.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maverick.Domain.Adapters
{
    public interface IContratoRefinanciamentoDbAdapter
    {
        /// <summary>
        /// Realiza a busca dos contratos de Refinanciamento aptos para envio do SMS.
        /// </summary> 
        /// <returns>
        ///     Lista de contratos de refinanciamento aptos para envio de SMS.
        /// </returns>
        Task<IEnumerable<Contrato>> BuscarContratosAptosEnvioSMSAsync();

        /// <summary>
        /// Atualiza contratos com as tentivas de envio do SMS
        /// </summary>
        /// <param name="contratos">
        /// Lista de contrato para atualizar
        /// </param>
        Task AtualizarContratosEnvioSMSAsync(IEnumerable<Contrato> contratos);
    }
}
