using APIFilmes.Data.Dtos;
using APIFilmes.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIFilmes.Profiles
{
    public class FilmeProfile : Profile
    {
        //O auto mapper permite a "conversão" mais rápida de um tipo de objeto para outro
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
        }
    }
}
