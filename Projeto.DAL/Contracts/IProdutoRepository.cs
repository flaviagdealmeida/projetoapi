using System;
using System.Collections.Generic;
using System.Text;
using Projeto.Entities;

namespace Projeto.DAL.Contracts
{
    public interface IProdutoRepository:IBaseRepositoy<Produto>
    {

        List<Produto> GetByName(string nome);
        List<Produto> GetByPreco(decimal precoIni, decimal precoFim);


    }
}
