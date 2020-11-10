using AutoMapper;
using TesteInvillia.Application.Common.Mappings;
using TesteInvillia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Application.Amigos.Queries.GetAmigos
{
    public class AmigoDto : IMapFrom<Amigo>
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Amigo, AmigoDto>();                
        }
    }
}
