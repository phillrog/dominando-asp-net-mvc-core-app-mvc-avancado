using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Dev.IO.App.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevIO.Data.Contexts;
using Microsoft.AspNetCore.Mvc;
using DevIO.Business.Interfaces;
using DevIO.Data.Repositories;
using AutoMapper;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Dev.IO.App.Extensions;

namespace Dev.IO.App
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<MeuDbContext>(options =>
							options.UseSqlServer(
								Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services.AddAutoMapper(typeof(Startup));

			services.AddControllersWithViews(options => options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x,y) => "O valor preenchido é inválido para este campo." ));
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

			services.AddScoped<MeuDbContext>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();
			services.AddScoped<IEnderecoRepository, EnderecoRepository>();
			services.AddScoped<IFornecedorRepository, FornecedorRepository>();
			services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			var defaultCulture = new CultureInfo("pt-BR");
			var localizationOptions = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture( defaultCulture ),
				SupportedCultures = new List<CultureInfo> { defaultCulture },
				SupportedUICultures = new List<CultureInfo> { defaultCulture }
			};

			app.UseRequestLocalization(localizationOptions);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
