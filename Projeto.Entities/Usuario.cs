using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public override string ToString()
        {
            return $"Id: {IdUsuario}, Nome:{Nome}, Login:{Login}";
        }




    }
}
