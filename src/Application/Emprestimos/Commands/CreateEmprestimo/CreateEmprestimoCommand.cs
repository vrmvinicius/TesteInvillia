using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.Commands.CreateEmprestimo
{
    public class CreateEmprestimoCommand : IRequest<int>
    {
        public int AmigoId { get; set; }
        public int JogoId { get; set; }
    }

    public class CreateEmprestimoCommandHandler : IRequestHandler<CreateEmprestimoCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateEmprestimoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateEmprestimoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Emprestimo
            {
                AmigoId = request.AmigoId,
                JogoId = request.JogoId,                
                Devolvido = false
            };

            entity.DomainEvents.Add(new EmprestimoCreatedEvent(entity));

            _context.Emprestimos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
