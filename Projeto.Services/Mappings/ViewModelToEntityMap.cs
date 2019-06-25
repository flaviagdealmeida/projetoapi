using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Projeto.Entities;
using Projeto.Services.Models;
using Projeto.Util;

namespace Projeto.Services.Mappings
{
    public class ViewModelToEntityMap : Profile
    {
        public ViewModelToEntityMap()
        {
            CreateMap<ProdutoCadastroViewModel, Produto>();
            CreateMap<ProdutoEdicaoViewModel, Produto>();
            CreateMap<UsuarioCadastroViewModel, Usuario>()
                .AfterMap((src,dest) => 
                    dest.Senha = Criptografia.GetMd5(src.Senha)
                );
        }

    }
}
