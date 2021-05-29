using System;
using System.Threading.Tasks;

namespace Maverick.Domain.Services
{
    public interface IContratosRefinanciamentoService
    {
        /// <summary>
        /// Realiza o envio do SMS para contratos de Refinanciamento
        /// que obtiveram mudança de saldo na hora da realização do pagamento
        /// </summary> 
        /// <returns>
        ///    Retorna positivo caso a quantidade parametrizada para execução do envio de SMS
        ///    tenha sido finalizada
        /// </returns>
        /// <exception cref="Exception" />
        Task<bool> EnviarSMSContratosRefinanciamentosAsync();
    }
}
