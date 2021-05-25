using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Application.UseCases
{
    public class MaintainTipoDeDisponibilidade
    {

        Application.Services.TipoDeDisponibilidade services;

        public MaintainTipoDeDisponibilidade(string pconexao)
        {
            services = new Application.Services.TipoDeDisponibilidade(pconexao);
        }

        public IEnumerable<Domain.Entity.TipoDeDisponibilidade> getAll()
        {
            return services.getAll();
        }

        public void post(Domain.Entity.TipoDeDisponibilidade entity)
        {
            services.post(entity);
        }

        public Domain.Entity.TipoDeDisponibilidade get(int Id)
        {
            return services.get(Id);
        }

        public void remove(Domain.Entity.TipoDeDisponibilidade entity)
        {
            services.remove(entity);
        }

        public void put(Domain.Entity.TipoDeDisponibilidade entity)
        {
            services.put(entity);
        }

    }
}
