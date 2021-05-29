using System.Threading.Tasks;

namespace Maverick.Domain.Adapters
{
    public interface IGerenciadorSMSAdapter
    {
        /// <summary>
        /// Realiza o envio do SMS
        /// </summary>
        /// <param name="mensagem">Mensagem do SMS</param>
        /// <param name="celular">Número do celular que será enviado o SMS</param>
        /// <returns>
        ///    Retorna true para caso tenha sucesso
        ///    Retorna false para caso não consiga enviar o SMS
        /// </returns>
        Task<bool> EnviarMensagemSMSAsync(string mensagem, string celular);
    }
}
