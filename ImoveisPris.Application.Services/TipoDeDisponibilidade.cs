using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Application.Services
{
    public class TipoDeDisponibilidade
    {
        Infra.Services.TipoDeDisponibilidade infraServices;
        Domain.Services.TipoDeDisponibilidade domainServices;
        Infra.Services.Imovel infraServicesImoveis;

        public TipoDeDisponibilidade(string pconexao)
        {
            infraServices = new (pconexao);
            domainServices = new ();
            infraServicesImoveis = new(pconexao);
        }

        public IList<Domain.Entity.TipoDeDisponibilidade> getAll()
        {
            return infraServices.getAll();
        }

        public void post(Domain.Entity.TipoDeDisponibilidade entity)
        {
            entity.Validate();
            domainServices.VerifyDescricaoExist(infraServices.SearchByDescription(entity));
            infraServices.add(entity);
        }

        public Domain.Entity.TipoDeDisponibilidade get(int Id)
        {
            return infraServices.get(Id);
        }

        public void remove(Domain.Entity.TipoDeDisponibilidade entity)
        {
            Domain.Entity.Imovel imovel = new();
            Domain.Services.Imovel domainServicesImovel = new();
            imovel.TipoDeDisponibilidadeId = entity.Id;
            domainServicesImovel.VerifyTipoDeDisponibilidadeExist(infraServicesImoveis.SearchByTipoDeDisponibilidade(imovel));
            infraServices.remove(entity);
        }

        public void put(Domain.Entity.TipoDeDisponibilidade entity)
        {
            entity.Validate();
            domainServices.VerifyDescricaoExist(infraServices.SearchByDescription(entity));
            infraServices.update(entity);
        }
    }
}
