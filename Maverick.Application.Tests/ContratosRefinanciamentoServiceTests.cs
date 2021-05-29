using Maverick.Domain.Adapters;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Otc.DomainBase.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Maverick.Application.Tests
{
    public class ContratosRefinanciamentoServiceTests
    {
        private readonly IContratosRefinanciamentoService  contratosRefinanciamentoService;
        private readonly Mock<IParametrosDbAdapter> parametrosDbAdapter;
        private readonly Mock<ILogDbAdapter> logDbAdapter;
        private readonly Mock<IContratoRefinanciamentoDbAdapter> contratoRefinanciamentoDbAdapter;
        private readonly Mock<IGerenciadorSMSAdapter> gerenciadorSMSAdapter;

        public ContratosRefinanciamentoServiceTests()
        {
            parametrosDbAdapter = new Mock<IParametrosDbAdapter>();
            logDbAdapter = new Mock<ILogDbAdapter>();
            contratoRefinanciamentoDbAdapter = new Mock<IContratoRefinanciamentoDbAdapter>();
            gerenciadorSMSAdapter = new Mock<IGerenciadorSMSAdapter>();


            contratosRefinanciamentoService = new ContratosRefinanciamento(
                contratoRefinanciamentoDbAdapter.Object,
                logDbAdapter.Object,
                gerenciadorSMSAdapter.Object,
                parametrosDbAdapter.Object);
        }

        [Fact]
        [Trait(nameof(IContratosRefinanciamentoService.EnviarSMSContratosRefinanciamentosAsync), "Sucesso")]
        public async Task ObterFilmesAsync_Sucesso()
        {
            bool expected = true;

            string mensagemSMSRetorno = "O SMS Enviado";

            var contratosRetorno = new List<Contrato>()
            {
                new Contrato()
                {
                    Numero = 123456,
                    Valores = new Valores()
                    {
                        SaldoDevedorAntigo = 333,
                        SaldoDevedorNovo = 334,
                        QuantidadeParcela = 12,
                        ValorParcela = 78,
                        TaxaJuros =4.4m
                    },
                    ContatoCliente = new ContatoCliente()
                    {
                        Celular = "31992585555"
                    }
                }
            };

            var sucessoTentativaEnvio = true;

            contratoRefinanciamentoDbAdapter
                .Setup(m => m.BuscarContratosAptosEnvioSMSAsync())
                .ReturnsAsync(contratosRetorno);

            logDbAdapter
                .Setup(m => m.SalvarLogAsync(It.IsAny<LogContrato>()));               

            parametrosDbAdapter
                .Setup(m => m.BuscarMensagemSMSAsync())
                .ReturnsAsync(mensagemSMSRetorno);

            gerenciadorSMSAdapter
                .Setup(m => m.EnviarMensagemSMSAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(sucessoTentativaEnvio);

            contratoRefinanciamentoDbAdapter
                .Setup(m => m.AtualizarContratosEnvioSMSAsync(It.IsAny<IEnumerable<Contrato>>()));

            logDbAdapter
                .Setup(m => m.SalvarLogAsync(It.IsAny<LogContrato>()));                

            var retorno = await contratosRefinanciamentoService.EnviarSMSContratosRefinanciamentosAsync();

            Assert.Equal(retorno, expected);
        }
    }
}
