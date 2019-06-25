using Projeto.BLL.Contracts;
using Projeto.DAL.Contracts;
using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.BLL.Business
{
    public class UsuarioBusiness : IUsuarioBusiness
    {

        private readonly IUsuarioRepository repository;

        public UsuarioBusiness(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public void Cadastrar(Usuario obj)
        {
            if (repository.HasLogin(obj.Login))
            
                throw new Exception("Login ja foi cadastrado! Tente outro.");
            else
                repository.Create(obj);
        }

        public void Atualizar(Usuario obj)
        {
            repository.Update(obj);
        }

        public void Excluir(int id)
        {
            repository.Remove(id);
        }

        public Usuario ConsultarPorId(int id)
        {
            return repository.GetById(id);
        }

        public List<Usuario> ConsultarTodos()
        {
            return repository.GetAll();
        }

        public Usuario Obter(string login, string senha)
        {
            return repository.Find(login, senha);
        }
    }
}
