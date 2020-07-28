using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dev.IO.App.Extensions.Seguranca
{
	public class ClaimsAuthorizeAttribute : TypeFilterAttribute
	{
		public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
		{
			Arguments = new object[] { new Claim(claimName, claimValue) };
		}
	}
}
