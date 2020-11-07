using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Amigos.Commands.UpdateAmigo
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
