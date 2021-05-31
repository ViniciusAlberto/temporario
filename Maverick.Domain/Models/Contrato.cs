namespace Maverick.Domain.Models
{
    public class Contrato
    {

        public long Numero { get; set; }

        public Valores Valores { get; set; }

        public ContatoCliente ContatoCliente {get;set;}

        public bool Sucesso { get; set; }

        public bool Tentativa { get; set; }
    }
}
