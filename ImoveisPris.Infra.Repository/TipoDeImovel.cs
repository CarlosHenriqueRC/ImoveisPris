using System;
using ImoveisPris.Domain.Entity;
using ImoveisPris.Infra.Persistence;
using ImoveisPris.Infra.Services;
using System.Collections.Generic;

namespace ImoveisPris.Infra.Repository
{
    public class TipoDeImovel:IRepositoryTiposDeImovel
    {

        ImoveisPris.Infra.Services.Services services = new();

        public IList<Domain.Entity.TipoDeImovel> GetAllData()
        {

            IList<Persistence.TipoDeImovel> lPTipoDeImovel = new List<Persistence.TipoDeImovel>();
            IList<Domain.Entity.TipoDeImovel> lETipoDeImovel = new List<Domain.Entity.TipoDeImovel>();

            lPTipoDeImovel = services.GetAllData();

            foreach(Persistence.TipoDeImovel origem in lPTipoDeImovel)
            {
                Domain.Entity.TipoDeImovel destino = new();
                destino.Id = origem.Id;
                destino.Descricao = origem.Descricao;
            }

            return lETipoDeImovel;

        }
    }
}
