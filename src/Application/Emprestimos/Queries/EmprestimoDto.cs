using AutoMapper;
using TesteInvillia.Application.Amigos.Queries.GetAmigos;
using TesteInvillia.Application.Common.Mappings;
using TesteInvillia.Application.Jogos.Queries.GetJogos;
using TesteInvillia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Application.Emprestimos.Queries
{
    public class EmprestimoDto : IMapFrom<Emprestimo>
    {
        public int Id { get; set; }
        public int AmigoId { get; set; }
        public string AmigoNome { get; set; }
        public int JogoId { get; set; }
        public string JogoNome { get; set; }
        public bool Devolvido { get; set; }

        public void Mapping(Profile profile)
        {
            var entityToDto = profile.CreateMap<Emprestimo, EmprestimoDto>();
            
            entityToDto.ForMember(x => x.AmigoId, mc => mc.MapFrom(ent => ent.Amigo.Id));
            entityToDto.ForMember(x => x.AmigoNome, mc => mc.MapFrom(ent => ent.Amigo.Nome));

            entityToDto.ForMember(x => x.JogoId, mc => mc.MapFrom(ent => ent.Jogo.Id));
            entityToDto.ForMember(x => x.JogoNome, mc => mc.MapFrom(ent => ent.Jogo.Nome));
        }
    }
}
