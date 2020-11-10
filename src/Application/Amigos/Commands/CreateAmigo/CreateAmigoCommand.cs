using TesteInvillia.Application.Common.Interfaces;
using TesteInvillia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Amigos.Commands.CreateAmigo
{
    public class CreateAmigoCommand : IRequest<int>
    {
        public string Nome { get; set; }
    }

    public class CreateAmigoCommandHandler : IRequestHandler<CreateAmigoCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateAmigoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateAmigoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Amigo();

            entity.Nome = request.Nome;

            _context.Amigos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
