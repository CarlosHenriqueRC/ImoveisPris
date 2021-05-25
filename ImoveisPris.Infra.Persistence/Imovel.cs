using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Infra.Persistence
{
    public class Imovel
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public int CEP { get; set; }
        public int Quartos { get; set; }
        public int Banheiros { get; set; }
        public int Vagas { get; set; }
        public decimal Area { get; set; }
        public decimal Valor { get; set; }
        public string Observacoes { get; set; }
        public int TipoDeDisponibilidadeId { get; set; }
        public int TipoDeImovelId { get; set; }

        public int Suites { get; set; }

    }
}
