using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DevIO.Data.Contexts;

namespace DevIO.Data.Repositories
{
	public class ProdutoRepository : Repository<Produto>, IProdutoRepository
	{
		public ProdutoRepository(MeuDbContext db) : base(db)
		{

		}

		public async Task<Produto> ObterProdutoFornecedor(Guid id)
		{
			return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
		{
			return await Buscar(p => p.FornecedorId == fornecedorId);
		}

		public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedores()
		{
			return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
				.OrderBy(p => p.Nome).ToListAsync();
		}
	}
}
