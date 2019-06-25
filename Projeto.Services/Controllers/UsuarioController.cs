using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.BLL.Contracts;
using Projeto.Entities;
using Projeto.Services.Models;

namespace Projeto.Services.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioBusiness business;

        public UsuarioController(IUsuarioBusiness business)
        {
            this.business = business;
        }
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = Mapper.Map<Usuario>(model);
                    business.Cadastrar(usuario);
                    return Ok($"Usuario {usuario.Nome}, cadastrado com sucesso!");
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