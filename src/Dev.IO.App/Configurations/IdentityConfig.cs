using AutoMapper.Configuration;
using Dev.IO.App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Dev.IO.App.Configurations
{
	public static class IdentityConfig
	{
		public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration Configuration)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			return services;
		}
	}
}
