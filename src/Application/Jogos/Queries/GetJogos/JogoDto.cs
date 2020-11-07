using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Jogos.Queries.GetJogos
{
    public class JogoDto : IMapFrom<Jogo>
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Jogo, JogoDto>();
        }
    }
}
