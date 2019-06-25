using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using Projeto.Entities;
using Projeto.DAL.Contracts;
using System.Linq;

namespace Projeto.DAL.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {

        private string connectionString;

        public ProdutoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Produto obj)
        {
            var query = "INSERT INTO PRODUTO(NOME, PRECO, QUANTIDADE) VALUES(@Nome, @Preco, @Quantidade)";

            using(var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }
        }

        public void Update(Produto obj)
        {
            var query = "UPDATE PRODUTO SET NOME = @Nome, PRECO =  @Preco, QUANTIDADE= @Quantidade WHERE IDPRODUTO = @IdProduto";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }
        }

        public void Remove(int id)
        {
            var query = "DELETE FROM PRODUTO WHERE IDPRODUTO = @IdProduto";

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, new { IdProduto = id });
            }
        }


        public List<Produto> GetAll()
        {
            var query = "SELECT * FROM PRODUTO";

            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Produto>(query).ToList();
            }
        }

        public Produto GetById(int id)
        {
            var query = "SELECT * FROM PRODUTO WHERE IDPRODUTO = @IdProduto";

            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Produto>(query, new {IdProduto = id}).SingleOrDefault();
            }
        }

        public List<Produto> GetByName(string nome)
        {
            var query = "SELECT * FROM PRODUTO WHERE NOME Like @Nome";

            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Produto>(query, new {Nome = $"%{nome}%"}).ToList();
            }
        }

        public List<Produto> GetByPreco(decimal precoIni, decimal precoFim)
        {
            var query = "SELECT * FROM PRODUTO WHERE PRECO BETWEEN @PrecoIni AND @PrecoFim ";

            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Produto>(query, new { PrecoIni = precoIni, PrecoFim = precoFim}).ToList();
            }
        }

    }
}
