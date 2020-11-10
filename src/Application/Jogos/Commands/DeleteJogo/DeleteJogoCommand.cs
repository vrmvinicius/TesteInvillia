using TesteInvillia.Application.Common.Exceptions;
using TesteInvillia.Application.Common.Interfaces;
using TesteInvillia.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Jogos.Commands.DeleteJogo
{
    public class DeleteJogoCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteJogoCommandHandler : IRequestHandler<DeleteJogoCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteJogoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteJogoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jogos
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Jogo), request.Id);
            }

            _context.Jogos.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
