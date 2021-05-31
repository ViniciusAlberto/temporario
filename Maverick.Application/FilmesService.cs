using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maverick.Domain.Adapters;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Microsoft.Extensions.Logging;
using Otc.Validations.Helpers;

namespace Maverick.Application
{
    public class ContratosRefinanciamentoService : IContratosRefinanciamentoService
    {
        private readonly IContratoRefinanciamentoDbAdapter contratoRefinanciamentoDbAdapter;
        private readonly IParametrosDbAdapter parametrosDbAdapter;
        private readonly ILogDbAdapter logDbAdapter;
        private readonly IGerenciadorSMSAdapter gerenciadorSMSAdapter;

        public ContratosRefinanciamentoService(
            IContratoRefinanciamentoDbAdapter contratoRefinanciamentoDbAdapter,
            IParametrosDbAdapter parametrosDbAdapter,
            ILogDbAdapter logDbAdapter,
            IGerenciadorSMSAdapter gerenciadorSMSAdapter)
        {
            this.contratoRefinanciamentoDbAdapter = contratoRefinanciamentoDbAdapter ??
                throw new ArgumentNullException(nameof(contratoRefinanciamentoDbAdapter));

            this.parametrosDbAdapter = parametrosDbAdapter ??
                throw new ArgumentNullException(nameof(parametrosDbAdapter));

            this.logDbAdapter = logDbAdapter ??
                throw new ArgumentNullException(nameof(logDbAdapter));

            this.gerenciadorSMSAdapter = gerenciadorSMSAdapter ??
                throw new ArgumentNullException(nameof(gerenciadorSMSAdapter));
        }

        public async Task<bool> EnviarSMSContratosRefinanciamentosAsync()
        {
            try
            {
                var contratosAptosEnviarSMS = await contratoRefinanciamentoDbAdapter
                                                .BuscarContratosAptosEnvioSMSAsync();

                var mensagemSMS = await parametrosDbAdapter.BuscarMensagemSMSAsync();

                foreach(var contratoApto in contratosAptosEnviarSMS)
                {
                    var mensagemPreenchida = MontarMensagem(contratoApto, mensagemSMS);

                    contratoApto.Sucesso = await gerenciadorSMSAdapter
                        .EnviarMensagemSMSAsync(mensagemPreenchida, contratoApto.ContatoCliente.Celular);

                    contratoApto.Tentativa = true;
                }
            }
            catch(Exception e)
            {

                return false;
            }
        }

        private string MontarMensagem(Contrato contratoApto, string mensagem)
        {
            //TODO:FAZER

            return string.Format(mensagem, contratoApto.Valores.ValorParcela);
        }
       
    }
}
