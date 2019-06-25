using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.DAL.Contracts
{
    public interface IUsuarioRepository:IBaseRepositoy<Usuario>
    {

        bool HasLogin(string login);
        Usuario Find(string login, string senha);
    }
}
