using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Entity.VO
{
    public class ReturnSearchImovel
    {

        public IList<Domain.Entity.Imovel> imoveis { get; set; }
        public IList<TipoDeImovel> TiposDeImoveis { get; set; }
        public IList<TipoDeDisponibilidade> TiposDeDisponibilidade { get; set; }
    }
}
