using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Dev.IO.App.Extensions.Seguranca
{
	public class RequisitoClaimFilter : IAuthorizationFilter
	{
		private readonly Claim _claim;

		public RequisitoClaimFilter(Claim claim)
		{
			_claim = claim;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
				return;
			}

			if (!CustomAuthorization.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
			{
				context.Result = new StatusCodeResult(403);
			}
		}
	}
}
