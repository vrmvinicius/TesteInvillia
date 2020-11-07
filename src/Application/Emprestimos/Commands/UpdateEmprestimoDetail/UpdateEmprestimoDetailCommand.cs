using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.Commands.UpdateEmprestimoDetail
{
    public class UpdateEmprestimoDetailCommand : IRequest
    {
        public int Id { get; set; }
        public int AmigoId { get; set; }
        public int JogoId { get; set; }
    }

    public class UpdateEmprestimoDetailCommandHandler : IRequestHandler<UpdateEmprestimoDetailCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmprestimoDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateEmprestimoDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Emprestimos.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Emprestimo), request.Id);
            }

            entity.AmigoId = request.AmigoId;
            entity.JogoId = request.JogoId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
