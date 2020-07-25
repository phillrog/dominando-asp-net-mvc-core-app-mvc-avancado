using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repositories
{
	public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
	{
		public EnderecoRepository(MeuDbContext db) : base(db)
		{

		}

		public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
		{
			return await Db.Enderecos.AsNoTracking().FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
		}
	}
}
