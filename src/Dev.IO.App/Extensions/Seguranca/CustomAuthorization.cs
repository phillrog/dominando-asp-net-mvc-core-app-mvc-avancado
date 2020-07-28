using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.IO.App.Extensions.Seguranca
{
	public class CustomAuthorization
	{
		public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
		{
			return context.User.Identity.IsAuthenticated &&
				   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
		}
	}
}
