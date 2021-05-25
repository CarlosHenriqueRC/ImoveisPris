using System;
using ImoveisPris.Infra.Persistence;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ImoveisPris.Infra.Services
{
    public class Imovel
    {
        ImovelContext context;

        public Imovel(string pconexao)
        {
            context = new ImovelContext(pconexao);
        }

        public IList<Domain.Entity.Imovel> getAll()
        {

            IList<Persistence.Imovel> lsource = context.Imovel.ToList();
            IList<Domain.Entity.Imovel> ldestiny = new List<Domain.Entity.Imovel>();

            foreach (Persistence.Imovel source in lsource)
            {
                Domain.Entity.Imovel destiny = new();
                destiny.Id = source.Id;
                destiny.Logradouro = source.Logradouro;
                destiny.Area = source.Area;
                destiny.Bairro = source.Bairro;
                destiny.Banheiros = source.Banheiros;
                destiny.CEP = source.CEP;
                destiny.Complemento = source.Complemento;
                destiny.Numero = source.Numero;
                destiny.Observacoes = source.Observacoes;
                destiny.Quartos = source.Quartos;
                destiny.Valor = source.Valor;
                destiny.Vagas = source.Vagas;
                destiny.Suites = source.Suites;
                destiny.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
                destiny.TipoDeImovelId = source.TipoDeImovelId;

                ldestiny.Add(destiny);
            }

            return ldestiny;
        }

        public Domain.Entity.Imovel get(int Id)
        {

            Persistence.Imovel persistence = context.Imovel.Find(Id);

            Domain.Entity.Imovel entity = new();
            entity.Id = persistence.Id;
            entity.Logradouro = persistence.Logradouro;
            entity.Area = persistence.Area;
            entity.Bairro = persistence.Bairro;
            entity.Banheiros = persistence.Banheiros;
            entity.CEP = persistence.CEP;
            entity.Complemento = persistence.Complemento;
            entity.Numero = persistence.Numero;
            entity.Observacoes = persistence.Observacoes;
            entity.Quartos = persistence.Quartos;
            entity.Valor = persistence.Valor;
            entity.Vagas = persistence.Vagas;
            entity.Suites = persistence.Suites;
            entity.TipoDeDisponibilidadeId = persistence.TipoDeDisponibilidadeId;
            entity.TipoDeImovelId = persistence.TipoDeImovelId;

            return entity;

        }

        public int add(Domain.Entity.Imovel source)
        {
            Persistence.Imovel destiny = new();
            destiny.Logradouro = source.Logradouro;
            destiny.Area = source.Area;
            destiny.Bairro = source.Bairro;
            destiny.Banheiros = source.Banheiros;
            destiny.CEP = source.CEP;
            destiny.Complemento = source.Complemento;
            destiny.Numero = source.Numero;
            destiny.Observacoes = source.Observacoes;
            destiny.Quartos = source.Quartos;
            destiny.Valor = source.Valor;
            destiny.Vagas = source.Vagas;
            destiny.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
            destiny.TipoDeImovelId = source.TipoDeImovelId;
            destiny.Suites = source.Suites;
            context.Imovel.Add(destiny);
            context.SaveChanges();
            return destiny.Id;
        }

        public void update(Domain.Entity.Imovel source)
        {

            Persistence.Imovel persistence = context.Imovel.Find(source.Id);
            persistence.Logradouro = source.Logradouro;
            persistence.Area =source.Area;
            persistence.Bairro =source.Bairro;
            persistence.Banheiros =source.Banheiros;
            persistence.CEP =source.CEP;
            persistence.Complemento =source.Complemento;
            persistence.Numero=source.Numero;
            persistence.Observacoes =source.Observacoes;
            persistence.Quartos =source.Quartos;
            persistence.Valor = source.Valor;
            persistence.Vagas = source.Vagas;
            persistence.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
            persistence.TipoDeImovelId = source.TipoDeImovelId;
            persistence.Suites = source.Suites;
            context.Update(persistence);
            context.SaveChanges();
        }

        public void remove(Domain.Entity.Imovel entity)
        {
            Persistence.Imovel persistence = context.Imovel.Find(entity.Id);
            context.Imovel.Remove(persistence);
            context.SaveChanges();
        }

        public IList<Domain.Entity.Imovel> Search(Domain.Entity.VO.SearchImovel search)
        {

            string bairro = "%" + search.Bairro  +"%";

            IList<Domain.Entity.Imovel> imoveis =new List<Domain.Entity.Imovel>();
            var list = context.Imovel
                                .Where(x => x.TipoDeDisponibilidadeId == search.TipoDeDisponibilidadeId)
                                .Where(x => x.TipoDeImovelId == search.TipoImovelId)
                                .Where(x=> x.Quartos>=search.NumeroDeQuartos && x.Quartos<=search.NumeroDeQuartose)
                                .Where(x => x.Vagas >= search.NumeroDeVagas && x.Vagas <= search.NumeroDeVagase)
                                .Where(x => x.Valor >= search.Valor && x.Valor <= search.Valore)
                                .Where(x => x.Suites >= search.NumeroDeSuites && x.Suites <= search.NumeroDeSuitese)
                                .Where(x=>EF.Functions.Like (x.Bairro, bairro))
                                .ToList()
                                .OrderByDescending(x=>x.Valor);

            foreach (Persistence.Imovel source in list)
            {
                Domain.Entity.Imovel destiny = new();
                destiny.Id = source.Id;
                destiny.Logradouro = source.Logradouro;
                destiny.Area = source.Area;
                destiny.Bairro = source.Bairro;
                destiny.Banheiros = source.Banheiros;
                destiny.CEP = source.CEP;
                destiny.Complemento = source.Complemento;
                destiny.Numero = source.Numero;
                destiny.Observacoes = source.Observacoes;
                destiny.Quartos = source.Quartos;
                destiny.Valor = source.Valor;
                destiny.Vagas = source.Vagas;
                destiny.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
                destiny.TipoDeImovelId = source.TipoDeImovelId;
                destiny.Suites = source.Suites;
                imoveis.Add(destiny);
            }

            return imoveis;
        }

        public IList<Domain.Entity.Imovel> SearchByCharacteristics(Domain.Entity.Imovel entity)
        {


            IList<Domain.Entity.Imovel> imoveis = new List<Domain.Entity.Imovel>();
            var list = context.Imovel
                                .Where(x => x.TipoDeDisponibilidadeId == entity.TipoDeDisponibilidadeId)
                                .Where(x => x.TipoDeImovelId == entity.TipoDeImovelId)
                                .Where(x => x.Logradouro == entity.Logradouro.Trim())
                                .Where(x => x.Numero == entity.Numero)
                                .Where(x => x.CEP == entity.CEP)
                                .Where(x => x.Complemento == entity.Complemento.Trim())
                                .Where(x=> x.Bairro == entity.Bairro.Trim())
                                .ToList();

            foreach (Persistence.Imovel source in list)
            {
                Domain.Entity.Imovel destiny = new();
                destiny.Id = source.Id;
                destiny.Logradouro = source.Logradouro;
                destiny.Area = source.Area;
                destiny.Bairro = source.Bairro;
                destiny.Banheiros = source.Banheiros;
                destiny.CEP = source.CEP;
                destiny.Complemento = source.Complemento;
                destiny.Numero = source.Numero;
                destiny.Observacoes = source.Observacoes;
                destiny.Quartos = source.Quartos;
                destiny.Valor = source.Valor;
                destiny.Vagas = source.Vagas;
                destiny.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
                destiny.TipoDeImovelId = source.TipoDeImovelId;
                destiny.Suites = source.Suites;
                imoveis.Add(destiny);
            }

            return imoveis;
        }

        public IList<Domain.Entity.Imovel> SearchByTipoDeImovel(Domain.Entity.Imovel entity)
        {

            IList<Domain.Entity.Imovel> imoveis = new List<Domain.Entity.Imovel>();
            var list = context.Imovel
                                .Where(x => x.TipoDeImovelId == entity.TipoDeImovelId)
                                .ToList();

            foreach (Persistence.Imovel source in list)
            {
                Domain.Entity.Imovel destiny = new();
                destiny.Id = source.Id;
                destiny.Logradouro = source.Logradouro;
                destiny.Area = source.Area;
                destiny.Bairro = source.Bairro;
                destiny.Banheiros = source.Banheiros;
                destiny.CEP = source.CEP;
                destiny.Complemento = source.Complemento;
                destiny.Numero = source.Numero;
                destiny.Observacoes = source.Observacoes;
                destiny.Quartos = source.Quartos;
                destiny.Valor = source.Valor;
                destiny.Vagas = source.Vagas;
                destiny.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
                destiny.TipoDeImovelId = source.TipoDeImovelId;
                destiny.Suites = source.Suites;
                imoveis.Add(destiny);
            }

            return imoveis;
        }

        public IList<Domain.Entity.Imovel> SearchByTipoDeDisponibilidade(Domain.Entity.Imovel entity)
        {


            IList<Domain.Entity.Imovel> imoveis = new List<Domain.Entity.Imovel>();
            var list = context.Imovel
                                .Where(x => x.TipoDeDisponibilidadeId == entity.TipoDeDisponibilidadeId)
                                .ToList();

            foreach (Persistence.Imovel source in list)
            {
                Domain.Entity.Imovel destiny = new();
                destiny.Id = source.Id;
                destiny.Logradouro = source.Logradouro;
                destiny.Area = source.Area;
                destiny.Bairro = source.Bairro;
                destiny.Banheiros = source.Banheiros;
                destiny.CEP = source.CEP;
                destiny.Complemento = source.Complemento;
                destiny.Numero = source.Numero;
                destiny.Observacoes = source.Observacoes;
                destiny.Quartos = source.Quartos;
                destiny.Valor = source.Valor;
                destiny.Vagas = source.Vagas;
                destiny.TipoDeDisponibilidadeId = source.TipoDeDisponibilidadeId;
                destiny.TipoDeImovelId = source.TipoDeImovelId;
                destiny.Suites = source.Suites;
                imoveis.Add(destiny);
            }

            return imoveis;
        }


        public decimal valorMaximoImovel()
        {
            return context.Imovel.Max(x => x.Valor);
        }
    }
}
