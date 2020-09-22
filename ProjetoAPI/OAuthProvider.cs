using Microsoft.Owin.Security.OAuth;
using ProjetoAPI.Models;
using ProjetoAPI.Models.Context;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ProjetoAPI
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {

        BancoContext db = new BancoContext();

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {

                string email = context.UserName;
                string password = context.Password;

                Usuario user = new Usuario().Get(email, password);

                if (user != null)
                {

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Nome),
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.UsuarioAdm),
                        new Claim(ClaimTypes.Role, user.Ativo)
                };
                    ClaimsIdentity OAuthIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);

                    context.Validated(new Microsoft.Owin.Security.AuthenticationTicket(OAuthIdentity, new Microsoft.Owin.Security.AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("Erro", "Os dados passados como username e password estão incorretos!");
                }
            });
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}