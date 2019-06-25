using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projeto.Services.Models;
using Projeto.BLL.Contracts;
using Projeto.Util;

namespace Projeto.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUsuarioBusiness business;

        public LoginController(IUsuarioBusiness business)
        {
            this.business = business;
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] LoginViewModel model,
            [FromServices]TokenConfiguration tokenConfiguration, //aspnet passa esses codigos
            [FromServices] LoginConfiguration loginConfiguration)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var usuario = business.Obter
                          (model.Usuario, Criptografia.GetMd5(model.Senha));


                    if (usuario != null)
                    {
                        //criando as credenciais do usuario..
                        ClaimsIdentity identity = new ClaimsIdentity(
                                new GenericIdentity(model.Usuario, "Login"),
                                new[]
                                {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                new Claim(JwtRegisteredClaimNames.UniqueName, model.Usuario)
                                }
                            );

                        //gerando o token
                        var dataCriacao = DateTime.Now;
                        var dataExpiracao = dataCriacao + TimeSpan.FromSeconds
                        (tokenConfiguration.Seconds);

                        var handler = new JwtSecurityTokenHandler();
                        var securityToken = handler.CreateToken(new
                        SecurityTokenDescriptor
                        {
                            Issuer = tokenConfiguration.Issuer,
                            Audience = tokenConfiguration.Audience,
                            SigningCredentials = loginConfiguration
                            .SigningCredentials,
                            Subject = identity,
                            NotBefore = dataCriacao,
                            Expires = dataExpiracao
                        });

                        var token = handler.WriteToken(securityToken); //CRIADO!!

                        var result = new
                        {
                            authenticated = true,
                            created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                            expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                            accessToken = token,
                            message = "OK"
                        };

                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Usuário inválido.");
                    }
                }
                catch (Exception e)
                {

                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest("Ocorreram erros de validação");

            }

        }
    }
}