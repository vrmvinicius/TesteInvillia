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

namespace TesteInvillia.Application.Amigos.Commands.DeleteAmigo
{
    public class DeleteAmigoCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteAmigoCommandHandler : IRequestHandler<DeleteAmigoCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAmigoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAmigoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Amigos
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Amigo), request.Id);
            }

            _context.Amigos.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
