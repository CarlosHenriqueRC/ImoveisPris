using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Application.UseCases
{
    public class SearchImovel
    {
        Application.Services.TipoDeDisponibilidade servicesTD;
        Application.Services.TipoDeImovel servicesTI;
        Application.Services.Imovel servicesImovel;

        public SearchImovel(string conexao)
        {
            servicesTD = new(conexao);
            servicesTI = new(conexao);
            servicesImovel = new (conexao);
        }

        public Domain.Entity.VO.EnterImovel get(int Id)
        {
            Domain.Entity.VO.EnterImovel imovel = new()
            {
                TiposDeDisponibilidade = servicesTD.getAll().ToList(),
                TiposDeImoveis = servicesTI.getAll().ToList(),
                imovel = null,
            };

            if (imovel.TiposDeDisponibilidade.Count == 0 || imovel.TiposDeImoveis.Count == 0)
                throw new Application.UseCases.ApplicationUseCaseException("Tipo De Disponibilidade ou Tipo de Imovel sem registros");

            if (Id!=0)
            {
                imovel.imovel = servicesImovel.get(Id);
            }

            return imovel;
        }

        public Domain.Entity.VO.ReturnSearchImovel search(Domain.Entity.VO.SearchImovel search)
        {

            Domain.Entity.VO.ReturnSearchImovel Return = new()
            {

                imoveis = servicesImovel.search(search).ToList(),
                TiposDeDisponibilidade = servicesTD.getAll().ToList(),
                TiposDeImoveis = servicesTI.getAll().ToList(),

            };

            return Return;

        }
    }
}
