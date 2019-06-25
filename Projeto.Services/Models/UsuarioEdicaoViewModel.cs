using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class UsuarioEdicaoViewModel
    {

       
        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem")]
         public string SenhaConfirmacao { get; set; }

    }
}
