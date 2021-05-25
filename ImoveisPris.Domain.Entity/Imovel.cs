using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImoveisPris.Domain.Entity
{
    public class Imovel:EntityBase,IEntityBase
    {
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public int CEP { get; set; }
        public int Quartos { get; set; }
        public int Banheiros { get; set; }
        public int Vagas { get; set; }
        public decimal Area { get; set; }
        public decimal Valor { get; set; }
        public string Observacoes { get; set; }
        public int TipoDeDisponibilidadeId { get; set; }
        public int TipoDeImovelId { get; set; }
        public int Suites { get; set; }

        public  void Validate()
        {
            if (this.Logradouro.Trim().Length == 0)
                throw new DomainEntityValidateException("Logradouro irregular");
            if (this.Bairro.Trim().Length == 0)
                throw new DomainEntityValidateException("Bairro irregular");
            if (this.CEP == 0)
                throw new DomainEntityValidateException("CEP irregular");
            if (this.Area <= 0)
                throw new DomainEntityValidateException("Area irregular");
            if (this.Valor <= 0)
                throw new DomainEntityValidateException("Valor irregular");
            if (this.TipoDeDisponibilidadeId <= 0)
                throw new DomainEntityValidateException("Tipo de Disponibilidade irregular");
            if (this.TipoDeImovelId <= 0)
                throw new DomainEntityValidateException("Tipo de Imovel irregular");

        }
    }
}
