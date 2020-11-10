using TesteInvillia.Application.Common.Interfaces;
using TesteInvillia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Jogos.Commands.CreateJogo
{
    public class CreateJogoCommand : IRequest<int>
    {
        public string Nome { get; set; }
    }

    public class CreateJogoCommandHandler : IRequestHandler<CreateJogoCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateJogoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateJogoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Jogo();

            entity.Nome = request.Nome;

            _context.Jogos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
