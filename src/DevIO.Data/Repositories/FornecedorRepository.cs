using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repositories
{
	public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
	{
		public FornecedorRepository(MeuDbContext db) : base(db)
		{

		}

		public async Task<Fornecedor> ObterFornecedorEndereco(Guid Id)
		{
			return await Db.Fornecedores.AsNoTracking().Include(c => c.Endereco).FirstOrDefaultAsync(c => c.Id == Id);
		}

		public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid Id)
		{
			return await Db.Fornecedores
				.Include(f => f.Produtos)
				.Include(c => c.Endereco)
				.FirstOrDefaultAsync(f => f.Id == Id);
		}
	}
}
