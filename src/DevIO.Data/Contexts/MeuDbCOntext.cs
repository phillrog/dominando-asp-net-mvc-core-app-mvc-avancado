using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevIO.Data.Contexts
{
	public class MeuDbCOntext : DbContext
	{
		public MeuDbCOntext(DbContextOptions<MeuDbCOntext> options) : base(options)
		{

		}

		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Endereco> Enderecos { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
			{
				property.SetColumnType("varchar(100)");
			}

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbCOntext).Assembly);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
