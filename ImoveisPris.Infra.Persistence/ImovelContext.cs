using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;


namespace ImoveisPris.Infra.Persistence
{
    public class ImovelContext:DbContext
    {

        private string conexao;

        public ImovelContext(string pconexao)
        {
            conexao = pconexao;
        }

        public DbSet<TipoDeImovel> TipoDeImovel { get; set; }
        public DbSet<TipoDeDisponibilidade> TipoDeDisponibilidade { get; set; }
        public DbSet<Imovel> Imovel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conexao);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            base.OnModelCreating(modelBuilder);
        }

    }
}
