using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Entity.VO
{
    public class SearchImovel:EntityBase
    {
        public int TipoDeDisponibilidadeId { get; set; }
        public int TipoImovelId { get; set; }
        public string Bairro { get; set; }
        public int NumeroDeSuites { get; set; }
        public int NumeroDeSuitese { get; set; }
        public int NumeroDeQuartos { get; set; }
        public int NumeroDeQuartose { get; set; }
        public int NumeroDeVagas { get; set; }
        public int NumeroDeVagase { get; set; }
        public decimal Valor { get; set; }
        public decimal Valore { get; set; }


    }
}
