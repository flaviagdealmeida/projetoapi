using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class UsuarioCadastroViewModel
    {

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Senha { get; set; }

        [Compare("Senha",ErrorMessage = "Senhas não conferem")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string SenhaConfirmacao { get; set; }

    }
}
