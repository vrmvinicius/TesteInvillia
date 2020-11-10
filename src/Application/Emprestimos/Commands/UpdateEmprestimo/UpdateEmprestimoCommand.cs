using TesteInvillia.Application.Common.Exceptions;
using TesteInvillia.Application.Common.Interfaces;
using TesteInvillia.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Emprestimos.Commands.UpdateEmprestimo
{
    public class UpdateEmprestimoCommand : IRequest
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public bool Devolvido { get; set; }
    }

    public class UpdateEmprestimoCommandHandler : IRequestHandler<UpdateEmprestimoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmprestimoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEmprestimoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Emprestimos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Emprestimo), request.Id);
            }

            entity.JogoId = request.JogoId;
            entity.Devolvido = request.Devolvido;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
