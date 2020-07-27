using Dev.IO.App.Extensions;
using DevIO.Business.Interfaces;
using DevIO.Data.Contexts;
using DevIO.Data.Repositories;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace Dev.IO.App.Configurations
{
	public static class DependencyInjectionConfig
	{
		public static IServiceCollection ResolveDependencies(this IServiceCollection services)
		{
			services.AddScoped<MeuDbContext>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IEnderecoRepository, EnderecoRepository>();
			services.AddScoped<IFornecedorRepository, FornecedorRepository>();
			services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

			return services;
		}
	}
}
