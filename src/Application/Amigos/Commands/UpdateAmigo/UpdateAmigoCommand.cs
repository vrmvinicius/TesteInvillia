using TesteInvillia.Application.Common.Exceptions;
using TesteInvillia.Application.Common.Interfaces;
using TesteInvillia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Amigos.Commands.UpdateAmigo
{
    public class UpdateAmigoCommand : IRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class UpdateAmigoCommandHandler : IRequestHandler<UpdateAmigoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAmigoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAmigoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Amigos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Amigo), request.Id);
            }

            entity.Nome = request.Nome;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
