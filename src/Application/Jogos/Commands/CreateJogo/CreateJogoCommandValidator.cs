using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Jogos.Commands.CreateJogo
{
    public class CreateJogoCommandValidator : AbstractValidator<CreateJogoCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateJogoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome do jogo deve ser preenchido.")
                .MaximumLength(60).WithMessage("O nome do jogo não deve exceder 60 caracteres.")
                .MustAsync(BeUniqueName).WithMessage("O nome do jogo já esta cadastrado.");
        }

        public async Task<bool> BeUniqueName(string nome, CancellationToken cancellationToken)
        {
            return await _context.Jogos
                .AllAsync(l => l.Nome != nome);
        }
    }
}
