using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Services
{
    public class TiposDeImovel
    {
        public void VerifyDescricaoExist(IList<Domain.Entity.TipoDeImovel> TiposDeImovel)
        {
            if (TiposDeImovel.Count() > 0)
                throw new Domain.Entity.DomainEntityValidateException("Tipo de imovel cadastrado");
        }
    }
}
