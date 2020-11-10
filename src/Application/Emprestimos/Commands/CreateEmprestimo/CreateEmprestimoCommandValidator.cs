using TesteInvillia.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Emprestimos.Commands.CreateEmprestimo
{
    public class CreateEmprestimoCommandValidator : AbstractValidator<CreateEmprestimoCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateEmprestimoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(e => e.JogoId)                
                .GreaterThan(0).WithMessage("Um jogo deve ser informado.")
                .MustAsync(IsJogoDisponivel).WithMessage("Este jogo esta emprestado.");

            RuleFor(e => e.AmigoId)
                .GreaterThan(0).WithMessage("Um amigo deve ser informado.");
        }

        public async Task<bool> IsJogoDisponivel(int jogoId, CancellationToken cancellationToken)
        {
            var isEmprestado = await _context.Emprestimos.AnyAsync(e => e.JogoId == jogoId && !e.Devolvido);
            return !isEmprestado;
        }
    }
}
