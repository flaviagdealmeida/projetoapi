using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BLL.Contracts;
using Projeto.Entities;
using Projeto.Services.Models;
using System;
using System.Collections.Generic;

namespace Projeto.Services.Controllers
{
    [Authorize("Bearer")] //criando a restrição de acesso
    [Produces("application/json")]
    [Route("api/Produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoBusiness business;

        public ProdutoController(IProdutoBusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProdutoCadastroViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var produto = Mapper.Map<Produto>(model);
                    business.Cadastrar(produto);

                    return Ok($"Produto {produto.Nome}, cadastrado com sucesso");
                }
                catch (Exception e)
                {
                    //internal server error
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                //bad request
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody]ProdutoEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var produto = Mapper.Map<Produto>(model);
                    business.Atualizar(produto);

                    return Ok($"Produto {produto.Nome}, atualizado com sucesso");
                }
                catch (Exception e)
                {
                    //internal server error
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                //bad request
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                var produto = business.ConsultarPorId(id);
                business.Excluir(id);

                if (produto != null)
                {
                    business.Excluir(id);
                    return Ok($"Produto {produto.Nome}, excluído com sucesso");
                }
                else
                {

                    return BadRequest("Produto não encontrado");
                }


            }
            catch (Exception e)
            {
                //internal server error
                return StatusCode(500, e.Message);
            }

        }


        [HttpGet]
        public IActionResult GetAll()
        {

            try
            {
                var lista = business.ConsultarTodos();
                var model = Mapper.Map<List<ProdutoConsultaViewModel>>(lista);

                return Ok(model);
            }
            catch (Exception e)
            {
                //internal server error
                return StatusCode(500, e.Message);
            }

        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var produto = business.ConsultarPorId(id);
                var model = Mapper.Map<ProdutoConsultaViewModel>(produto);

                return Ok(model);
            }
            catch (Exception e)
            {
                //internal server error
                return StatusCode(500, e.Message);
            }

        }

    }
}