﻿using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Data.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
	{
		protected readonly MeuDbContext Db;
		protected readonly DbSet<TEntity> DbSet;

		protected Repository(MeuDbContext db)
		{
			Db = db;
			DbSet = db.Set<TEntity>();
		}

		public virtual async Task Adicionar(TEntity entity)
		{
			DbSet.Add(entity);
			await SaveChanges();
		}

		public virtual async Task Atualizar(TEntity entity)
		{
			Db.Entry<TEntity>(entity).State = EntityState.Modified;
			DbSet.Update(entity);
			await SaveChanges();
		}

		public virtual async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual void Dispose()
		{
			Db?.Dispose();
		}

		public virtual async Task<TEntity> ObterPorId(Guid id)
		{
			return await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
		}

		public virtual async Task<List<TEntity>> ObterTodos()
		{
			return await DbSet.AsNoTracking().ToListAsync();
		}

		public virtual async Task Remover(Guid id)
		{
			//DbSet.Remove(await DbSet.FindAsync());
			DbSet.Remove(new TEntity { Id = id });
			await SaveChanges();
		}

		public virtual async Task<int> SaveChanges()
		{
			return await Db.SaveChangesAsync();
		}
	}
}
