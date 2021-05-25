using System;
using System.Collections.Generic;
using ImoveisPris.Domain.Entity;
using ImoveisPris.Application.Services;

namespace ImoveisPris.Application.UseCases
{
    public class MaintainImovel
    {

        Application.Services.Imovel servicesImovel;
        Application.Services.TipoDeDisponibilidade servicesTD;
        Application.Services.TipoDeImovel servicesTI;

        public MaintainImovel(string conexao)
        {
            servicesImovel = new(conexao);
            servicesTD = new (conexao);
            servicesTI = new(conexao);
        }

        public IList<Domain.Entity.Imovel> getAllImovel()
        {

            if (servicesTD.getAll().Count == 0 || servicesTI.getAll().Count == 0)
                throw new Application.UseCases.ApplicationUseCaseException("Tipo De Disponibilidade ou Tipo de Imovel sem registros");

            return servicesImovel.getAll();
        }

        public Domain.Entity.VO.EnterImovel getImovel(int Id)
        {
            Domain.Entity.VO.EnterImovel imovel = new()
            {
                TiposDeDisponibilidade = servicesTD.getAll(),
                TiposDeImoveis = servicesTI.getAll(),
                imovel = null,
            };

            if (imovel.TiposDeDisponibilidade.Count == 0 || imovel.TiposDeImoveis.Count == 0)
                throw new Application.UseCases.ApplicationUseCaseException("Tipo De Disponibilidade ou Tipo de Imovel sem registros");


            if (Id!=0) {
                imovel.imovel = servicesImovel.get(Id);
            }


            return imovel;
        }

        public int post(Domain.Entity.Imovel entity)
        {
            return servicesImovel.post(entity);
        }

        public void put(Domain.Entity.Imovel entity)
        {
            servicesImovel.put(entity);
        }

        public void remove(Domain.Entity.Imovel entity)
        {
            servicesImovel.remove(entity);
        }

    }
}
