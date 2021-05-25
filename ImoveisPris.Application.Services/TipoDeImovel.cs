using System;
using System.Collections.Generic;
using ImoveisPris.Domain.Entity;
using ImoveisPris.Infra.Services;

namespace ImoveisPris.Application.Services
{
    public class TipoDeImovel
    {

        Infra.Services.TipoDeImovel infraServices;
        Domain.Services.TiposDeImovel domainService;
        Infra.Services.Imovel infraServicesImoveis;

        public TipoDeImovel(string pconexao)
        {
            infraServices = new Infra.Services.TipoDeImovel(pconexao);
            domainService = new();
            infraServicesImoveis = new(pconexao);
        }

        public IList<Domain.Entity.TipoDeImovel> getAll()
        {
            return infraServices.getAll();
        }

        public void post(Domain.Entity.TipoDeImovel eTipoDeImovel)
        {
            eTipoDeImovel.Validate();
            domainService.VerifyDescricaoExist(infraServices.SearchByDescription(eTipoDeImovel));
            infraServices.add(eTipoDeImovel);
        }

        public Domain.Entity.TipoDeImovel  get(int Id)
        {
            return infraServices.get(Id);
        }

        public void remove(Domain.Entity.TipoDeImovel entity)
        {
            Domain.Entity.Imovel imovel = new();
            Domain.Services.Imovel domainServicesImovel = new();
            imovel.TipoDeImovelId = entity.Id;
            domainServicesImovel.VerifyTipoDeImovelExist(infraServicesImoveis.SearchByTipoDeImovel(imovel));
            infraServices.remove(entity);
        }

        public void put(Domain.Entity.TipoDeImovel eTipoDeImovel)
        {
            eTipoDeImovel.Validate();
            domainService.VerifyDescricaoExist(infraServices.SearchByDescription(eTipoDeImovel));
            infraServices.update(eTipoDeImovel);
        }
    }
}
