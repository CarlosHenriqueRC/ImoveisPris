using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImoveisPris.Domain.Entity;

namespace ImoveisPris.Domain.Entity.VO
{
    public class EnterImovel:EntityBase
    {

        public Imovel imovel { get; set; }
        public IList<TipoDeImovel> TiposDeImoveis { get; set; }
        public IList<TipoDeDisponibilidade> TiposDeDisponibilidade { get; set; }

    }
}
