using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.BLL.Contracts
{
    public interface IUsuarioBusiness:IBaseBusiness<Usuario>
    {
        Usuario Obter(string login, string senha);

    }
}
