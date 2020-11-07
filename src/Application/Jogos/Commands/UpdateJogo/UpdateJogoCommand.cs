using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Jogos.Commands.UpdateJogo
{
    public class UpdateJogoCommand : IRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class UpdateJogoCommanddHandler : IRequestHandler<UpdateJogoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateJogoCommanddHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateJogoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Jogos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Jogo), request.Id);
            }

            entity.Nome = request.Nome;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
