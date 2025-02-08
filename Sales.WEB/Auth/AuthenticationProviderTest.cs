using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sales.WEB.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);
            var anonimous = new ClaimsIdentity();
            //var zuluUser = new ClaimsIdentity(new List<Claim>
            //{
            //    new Claim("FirstName", "Juan"),
            //    new Claim("LastName", "Zulu"),
            //    new Claim(ClaimTypes.Name, "zulu@yopmail.com"),
            //    new Claim(ClaimTypes.Role, "Admin")
            //},
            //authenticationType: "test");
            //return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(zuluUser)));
            var adminUser = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Juan David"),
                new Claim("LastName", "Osorio Calderón"),
                new Claim(ClaimTypes.Name, "asesor1@avansis.com.co"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(adminUser)));
        }
    }
}
