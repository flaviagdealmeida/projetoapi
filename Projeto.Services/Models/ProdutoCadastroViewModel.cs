using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Projeto.Entities;

namespace Projeto.Services.Models
{
    public class ProdutoCadastroViewModel
    {
        [Required(ErrorMessage ="Campo Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int Quantidade { get; set; }


    }
}
