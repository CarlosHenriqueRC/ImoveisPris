using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Entity
{
    public class TipoDeDisponibilidade:EntityBase, IEntityBase
    {
        public string Descricao { get; set; }

        public void Validate()
        {
            if (this.Descricao.Trim().Length == 0)
                throw new DomainEntityValidateException("Descricao inválida");

        }
    }
}
