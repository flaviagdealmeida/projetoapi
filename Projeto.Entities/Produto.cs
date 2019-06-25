using System;

namespace Projeto.Entities
{
    public class Produto
    {

        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }



        public override string ToString()
        {
            return $"Id: {IdProduto}, Nome: {Nome}, Preco: {Preco}, Quantidade: {Quantidade}";
        }

    }
}
