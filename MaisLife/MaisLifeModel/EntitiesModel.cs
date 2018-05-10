using MaisLifeModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;

namespace MaisLifeModel
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EntitiesModel : DbContext
    {
        public DbSet<Bairro> Bairro { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<Carrinho_produto> Carrinho_produto { get; set; }
        public DbSet<Contato> Contato { get; set; }
        public DbSet<Devolucao> Devolucao { get; set; }
        public DbSet<Devolucao_produto> Devolucao_produto { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Entrada> Entrada { get; set; }
        public DbSet<Entrada_estoque> Entrada_estoque { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Mapa_pedido> Mapa_pedido { get; set; }
        public DbSet<Mapaentrega> Mapaentrega { get; set; }
        public DbSet<Parceiro> Parceiro { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Produto_bairro> Produto_bairro { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Usuario_externo> Usuario_externo { get; set; }

        public EntitiesModel()
      : base()
        {

        }

        public EntitiesModel(DbConnection existingConnection, bool contextOwnsConnection)
                : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Bairro>().MapToStoredProcedures();
            modelBuilder.Entity<Carrinho>().MapToStoredProcedures();
            modelBuilder.Entity<Carrinho_produto>().MapToStoredProcedures();
            modelBuilder.Entity<Contato>().MapToStoredProcedures();
            modelBuilder.Entity<Devolucao>().MapToStoredProcedures();
            modelBuilder.Entity<Devolucao_produto>().MapToStoredProcedures();
            modelBuilder.Entity<Endereco>().MapToStoredProcedures();
            modelBuilder.Entity<Entrada>().MapToStoredProcedures();
            modelBuilder.Entity<Entrada_estoque>().MapToStoredProcedures();
            modelBuilder.Entity<Estoque>().MapToStoredProcedures();
            modelBuilder.Entity<Fornecedor>().MapToStoredProcedures();
            modelBuilder.Entity<Mapa_pedido>().MapToStoredProcedures();
            modelBuilder.Entity<Mapaentrega>().MapToStoredProcedures();
            modelBuilder.Entity<Parceiro>().MapToStoredProcedures();
            modelBuilder.Entity<Pedido>().MapToStoredProcedures();
            modelBuilder.Entity<Produto>().MapToStoredProcedures();
            modelBuilder.Entity<Produto_bairro>().MapToStoredProcedures();
            modelBuilder.Entity<Usuario>().MapToStoredProcedures();
            modelBuilder.Entity<Usuario_externo>().MapToStoredProcedures();
        }
    }
}
