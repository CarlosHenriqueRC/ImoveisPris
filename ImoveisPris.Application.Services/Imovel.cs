using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Application.Services
{
    public class Imovel
    {

        Infra.Services.Imovel infraServices;
        Domain.Services.Imovel domainServices;

        public Imovel(string pconexao)
        {
            infraServices = new Infra.Services.Imovel(pconexao);
            domainServices = new();
        }
        public IList<Domain.Entity.Imovel> getAll()
        {
            return  infraServices.getAll();
        }

        public int post(Domain.Entity.Imovel entity)
        {
            entity.Validate();
            domainServices.VerifyImovelExist(entity.Id,infraServices.SearchByCharacteristics(entity));
            return infraServices.add(entity);
        }

        public Domain.Entity.Imovel get(int Id)
        {
            return infraServices.get(Id);
        }

        public void remove(Domain.Entity.Imovel entity)
        {
            infraServices.remove(entity);
        }

        public void put(Domain.Entity.Imovel entity)
        {
            entity.Validate();
            domainServices.VerifyImovelExist(entity.Id, infraServices.SearchByCharacteristics(entity));
            infraServices.update(entity);
        }

        public IList<Domain.Entity.Imovel> search(Domain.Entity.VO.SearchImovel search)
        {
            if (search.NumeroDeQuartose == 0)
                search.NumeroDeQuartose = int.MaxValue;
            if (search.NumeroDeSuitese == 0)
                search.NumeroDeSuitese = int.MaxValue;
            if (search.NumeroDeVagase == 0)
                search.NumeroDeVagase = int.MaxValue;
            if (search.Valore == 0)
                search.Valore = infraServices.valorMaximoImovel();
            return infraServices.Search(search);
        }
    }
}
