using Projeto.DAL.Contracts;
using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace Projeto.DAL.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string connectionString;



        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        
        public void Create(Usuario obj)
        {
            var query = "insert into Usuario(Nome, Login, Senha) values (@Nome, @Login, @Senha)";


            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }

        }

        public void Update(Usuario obj)
        {
            var query = "update Usuario set Nome = @Nome, Login = @Login, Senha = @Senha where IdUsuario = @IdUsuario";


            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, obj);
            }
        }

        public void Remove(int id)
        {
            var query = "delete from Usuario where IdUsuario = @IdUsuario";


            using (var conn = new SqlConnection(connectionString))
            {
                conn.Execute(query, new {idUsuario = id });
            }
        }
        public List<Usuario> GetAll()
        {
            var query = "select * from Usuario";


            using (var conn = new SqlConnection(connectionString))
            {
            return conn.Query<Usuario>(query).ToList();
            }
        }

        public Usuario GetById(int id)
        {
            var query = "select * from Usuario where IdUsuario = @IdUsuario";


            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Usuario>(query, new {IdUsuario = id}).SingleOrDefault();
            }
        }

        public bool HasLogin(string login)
        {
            var query = "select count(Login)from Usuario where Login = @Login";


            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<int>(query, new {Login = login }).SingleOrDefault() > 0;
            }
        }

        public Usuario Find(string login, string senha)
        {
            var query = "select * from Usuario where Login = @Login and Senha = @Senha";


            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Usuario>(query, new { Login = login, Senha = senha }).FirstOrDefault();
            }
        }

    }
}
