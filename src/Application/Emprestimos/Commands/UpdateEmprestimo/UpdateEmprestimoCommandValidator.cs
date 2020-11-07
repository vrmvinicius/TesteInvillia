using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.Commands.UpdateEmprestimo
{
    public class UpdateEmprestimoCommandValidator : AbstractValidator<UpdateEmprestimoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateEmprestimoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.JogoId)
                .MustAsync(IsJogoDisponivel).WithMessage("Este jogo já esta emprestado.");
        }

        public async Task<bool> IsJogoDisponivel(UpdateEmprestimoCommand model, int jogoId, CancellationToken cancellationToken)
        {            
            var isEmprestado = await _context.Emprestimos                
                .AnyAsync(e => e.Id != model.Id && e.JogoId == jogoId && !e.Devolvido);

            return !isEmprestado;
        }
    }
}
