using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Infra.Persistence
{
    public class TipoDeDisponibilidade
    {

        public TipoDeDisponibilidade()
        {
            this.imovel = new HashSet<Imovel>();
        }
        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Imovel> imovel { get; set; }

    }
}
