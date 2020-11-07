using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.Commands.CreateEmprestimo
{
    public class CreateEmprestimoCommandValidator : AbstractValidator<CreateEmprestimoCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateEmprestimoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.JogoId)                
                .MustAsync(IsJogoDisponivel).WithMessage("Este jogo esta emprestado.");
        }

        public async Task<bool> IsJogoDisponivel(int jogoId, CancellationToken cancellationToken)
        {
            var isEmprestado = await _context.Emprestimos.AnyAsync(e => e.JogoId == jogoId && !e.Devolvido);
            return !isEmprestado;
        }
    }
}
