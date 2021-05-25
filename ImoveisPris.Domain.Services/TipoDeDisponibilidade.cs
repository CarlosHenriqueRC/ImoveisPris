using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Services
{
    public class TipoDeDisponibilidade
    {
        public void VerifyDescricaoExist(IList<Domain.Entity.TipoDeDisponibilidade> TiposDeDisponibilidade)
        {
            if (TiposDeDisponibilidade.Count() > 0)
                throw new Domain.Entity.DomainEntityValidateException("Tipo de disponibilidade cadastrado");
        }
    }
}
