using System;
using System.Collections.Generic;
using System.Text;
using Projeto.Entities;

namespace Projeto.BLL.Contracts
{
    public interface IProdutoBusiness:IBaseBusiness<Produto>
    {
        List<Produto> ConsultarPorNome(string nome);
        List<Produto> ConsultarPorPreco(decimal precoIni, decimal precoFim);

    }
}
