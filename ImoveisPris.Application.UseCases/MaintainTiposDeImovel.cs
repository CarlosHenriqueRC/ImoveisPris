using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Application.UseCases
{
    public class MaintainTiposDeImovel
    {

        Application.Services.TipoDeImovel services;

        public MaintainTiposDeImovel(string pconexao)
        {
            services = new Application.Services.TipoDeImovel(pconexao);
        }

        public IEnumerable<Domain.Entity.TipoDeImovel> getAll()
        {
            return services.getAll();
        }

        public void post(Domain.Entity.TipoDeImovel eTipoDeImovel)
        {
            services.post(eTipoDeImovel);
        }

        public Domain.Entity.TipoDeImovel get(int Id)
        {
            return services.get(Id);
        }

        public void remove(Domain.Entity.TipoDeImovel eTipoDeImovel)
        {
            services.remove(eTipoDeImovel);
        }

        public void put(Domain.Entity.TipoDeImovel eTipoDeImovel)
        {
            services.put(eTipoDeImovel);
        }


    }
}
