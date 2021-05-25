using System;
using ImoveisPris.Infra.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace ImoveisPris.Infra.Services
{
    public class TipoDeImovel
    {

        ImovelContext context;

        public TipoDeImovel(string pconexao)
        {
            context = new ImovelContext(pconexao);

        }

        public IList<Domain.Entity.TipoDeImovel> getAll()
        {

            IList<Persistence.TipoDeImovel> lPersistence = context.TipoDeImovel.ToList();
            IList<Domain.Entity.TipoDeImovel> lEntity = new List<Domain.Entity.TipoDeImovel>();

            foreach (Persistence.TipoDeImovel source in lPersistence)
            {
                Domain.Entity.TipoDeImovel destiny = new();
                destiny.Id = source.Id;
                destiny.Descricao = source.Descricao;
                lEntity.Add(destiny);
            }

            return lEntity;
        }

        public IList<Domain.Entity.TipoDeImovel> SearchByDescription(Domain.Entity.TipoDeImovel entity)
        {
            var list = context.TipoDeImovel
                                .Where(x => x.Descricao == entity.Descricao)
                                .ToList();

            IList<Domain.Entity.TipoDeImovel> lEntity = new List<Domain.Entity.TipoDeImovel>();

            foreach (Persistence.TipoDeImovel source in list)
            {
                Domain.Entity.TipoDeImovel destiny = new();
                destiny.Id = source.Id;
                destiny.Descricao = source.Descricao;
                lEntity.Add(destiny);
            }

            return lEntity;
        }


        public Domain.Entity.TipoDeImovel get(int Id)
        {

            Persistence.TipoDeImovel persistence = context.TipoDeImovel.Find(Id);

            Domain.Entity.TipoDeImovel entity = new();
            entity.Id = persistence.Id;
            entity.Descricao = persistence.Descricao;

            return entity;

        }

        public void add(Domain.Entity.TipoDeImovel source)
        {
            source.Validate();
            Persistence.TipoDeImovel persistence = new();
            persistence.Descricao = source.Descricao;
            context.TipoDeImovel.Add(persistence);
            context.SaveChanges();
        }

        public void update(Domain.Entity.TipoDeImovel source)
        {
            source.Validate();
            Persistence.TipoDeImovel persistence = context.TipoDeImovel.Find(source.Id);
            persistence.Descricao = source.Descricao;            
            context.Update(persistence);
            context.SaveChanges();
        }

        public void remove(Domain.Entity.TipoDeImovel entity)
        {
            Persistence.TipoDeImovel persistence = context.TipoDeImovel.Find(entity.Id);
            context.TipoDeImovel.Remove(persistence);
            context.SaveChanges();
        }
    }
}
