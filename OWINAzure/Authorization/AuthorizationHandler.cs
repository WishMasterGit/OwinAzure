using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace OWINAzure.Authorization
{
    public class AuthorizationHeaderHandler
        : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var apiKey = request.RequestUri.ParseQueryString().Get("api_key");
            if (!string.IsNullOrEmpty(apiKey) && apiKey == "12345")
            {
                var username = "SuperUser";
                var usernameClaim = new Claim(ClaimTypes.Name, username);
                var userRoleClaim = new Claim(ClaimTypes.Role,"user");
                var identity = new ClaimsIdentity(new[] {usernameClaim,userRoleClaim}, "ApiKey");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}