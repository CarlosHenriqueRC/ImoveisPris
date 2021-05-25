using System;
using ImoveisPris.Infra.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace ImoveisPris.Infra.Services
{
    public class TipoDeDisponibilidade
    {
        ImovelContext context;

        public TipoDeDisponibilidade(string pconexao)
        {
            context = new ImovelContext(pconexao);
        }

        public IList<Domain.Entity.TipoDeDisponibilidade> getAll()
        {

            IList<Persistence.TipoDeDisponibilidade> lPersistence = context.TipoDeDisponibilidade.ToList();
            IList<Domain.Entity.TipoDeDisponibilidade> lEntity = new List<Domain.Entity.TipoDeDisponibilidade>();

            foreach (Persistence.TipoDeDisponibilidade source in lPersistence)
            {
                Domain.Entity.TipoDeDisponibilidade destiny = new();
                destiny.Id = source.Id;
                destiny.Descricao = source.Descricao;
                lEntity.Add(destiny);
            }

            return lEntity;
        }

        public Domain.Entity.TipoDeDisponibilidade get(int Id)
        {
            try { 
            Persistence.TipoDeDisponibilidade persistence = context.TipoDeDisponibilidade.Find(Id);
                if (persistence == null) throw new PersistenceEntityException("Tipo de Disponibilidade nao encontrado");
            Domain.Entity.TipoDeDisponibilidade entity = new();
            entity.Id = persistence.Id;
            entity.Descricao = persistence.Descricao;

            return entity;
            }
            catch
            {
                throw;
            }

        }

        public void add(Domain.Entity.TipoDeDisponibilidade source)
        {
            source.Validate();
            Persistence.TipoDeDisponibilidade persistence = new();
            persistence.Descricao = source.Descricao;
            context.TipoDeDisponibilidade.Add(persistence);
            context.SaveChanges();
        }

        public void update(Domain.Entity.TipoDeDisponibilidade source)
        {
            source.Validate();
            Persistence.TipoDeDisponibilidade persistence = context.TipoDeDisponibilidade.Find(source.Id);
            persistence.Descricao = source.Descricao;
            context.Update(persistence);
            context.SaveChanges();
        }

        public void remove(Domain.Entity.TipoDeDisponibilidade entity)
        {
            Persistence.TipoDeDisponibilidade persistence = context.TipoDeDisponibilidade.Find(entity.Id);
            context.TipoDeDisponibilidade.Remove(persistence);
            context.SaveChanges();
        }

        public IList<Domain.Entity.TipoDeDisponibilidade> SearchByDescription(Domain.Entity.TipoDeDisponibilidade entity)
        {

            var list = context.TipoDeDisponibilidade
                    .Where(x => x.Descricao == entity.Descricao)
                    .ToList();

            IList<Domain.Entity.TipoDeDisponibilidade> lEntity = new List<Domain.Entity.TipoDeDisponibilidade>();

            foreach (Persistence.TipoDeDisponibilidade source in list)
            {
                Domain.Entity.TipoDeDisponibilidade destiny = new();
                destiny.Id = source.Id;
                destiny.Descricao = source.Descricao;
                lEntity.Add(destiny);
            }

            return lEntity;
        }
    }
}
