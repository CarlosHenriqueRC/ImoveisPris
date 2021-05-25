using System;
using ImoveisPris.Domain.Entity;
using System.Collections.Generic;

namespace ImoveisPris.Domain.Services
{
    public class Imovel
    {

        public void VerifyTipoDeDisponibilidadeExist(IList<Domain.Entity.Imovel> imoveis)
        {
            if (imoveis.Count > 0)
                throw new Domain.Entity.DomainEntityValidateException("Existem imoveis com este Tipo de Disponibilidade");
        }

        public void VerifyTipoDeImovelExist(IList<Domain.Entity.Imovel> imoveis)
        {
            if (imoveis.Count > 0)
                throw new Domain.Entity.DomainEntityValidateException("Existem imoveis com este Tipo de Imovel");
        }

        public void VerifyImovelExist(int id, IList<Domain.Entity.Imovel> imoveis)
        {
            if ((imoveis.Count > 0) || (imoveis.Count == 1 && imoveis[0].Id == id))
                throw new Domain.Entity.DomainEntityValidateException("Imovel cadastrado");
            
        }


    }
}
