using System;

namespace ImoveisPris.Domain.Entity
{
    public class TipoDeImovel: EntityBase, IEntityBase
    {
        public string Descricao { get; set; }

        public void  Validate()
        {
            if (this.Descricao.Trim().Length == 0)
                throw new DomainEntityValidateException("Descricao inválida");

        }


    }
}
