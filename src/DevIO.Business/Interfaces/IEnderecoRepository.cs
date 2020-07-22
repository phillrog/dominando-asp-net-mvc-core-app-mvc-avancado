using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
	public interface IEnderecoRepository: IRepository<Fornecedor>
	{
		Task<Fornecedor> ObterEnderecoPorFornecedor(Guid fornecedorId);
		Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid Id);
	}
}
