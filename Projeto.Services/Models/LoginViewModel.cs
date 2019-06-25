using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Senha { get; set; }
    }
}
