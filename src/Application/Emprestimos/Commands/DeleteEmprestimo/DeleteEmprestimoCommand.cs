using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.Commands.DeleteEmprestimo
{
    public class DeleteEmprestimoCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteEmprestimoCommandHandler : IRequestHandler<DeleteEmprestimoCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmprestimoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEmprestimoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Emprestimos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Emprestimo), request.Id);
            }

            _context.Emprestimos.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
