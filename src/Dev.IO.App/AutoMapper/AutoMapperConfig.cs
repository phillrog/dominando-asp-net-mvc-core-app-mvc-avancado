using AutoMapper;
using Dev.IO.App.ViewModels;
using DevIO.Business.Models;

namespace Dev.IO.App.AutoMapper
{
	public class AutoMapperConfig : Profile
	{
		public AutoMapperConfig()
		{
			CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
			CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
			CreateMap<Produto, ProdutoViewModel>().ReverseMap();
		}
	}
}
