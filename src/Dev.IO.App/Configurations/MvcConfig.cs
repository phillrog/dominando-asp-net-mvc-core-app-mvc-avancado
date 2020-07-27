using Microsoft.Extensions.DependencyInjection;

namespace Dev.IO.App.Configurations
{
	public static class MvcConfig
	{
		public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
		{
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido é inválido para este campo."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor((x) => "Este campo precisa ser preenchido."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido é inválido para este campo."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico"));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor preenchido é inválido para este campo."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor((x) => "O campo deve ser numérico."));
			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => "O campo deve ser numérico."));

			services.AddRazorPages();

			return services;
		}
	}
}
