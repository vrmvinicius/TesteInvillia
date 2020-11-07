using CleanArchitecture.Application.Amigos.Queries.GetAmigos;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Jogos.Queries.GetJogos;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Emprestimos.Queries
{
    public class EmprestimoDto : IMapFrom<Emprestimo>
    {
        public int Id { get; set; }
        public AmigoDto AmigoDto  { get; set; }
        public JogoDto JogoDto { get; set; }
        public bool Devolvido { get; set; }

        public EmprestimoDto()
        {
        }
    }
}
