using TesteInvillia.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Application.Jogos.Commands.UpdateJogo
{
    public class UpdateJogoCommandValidator : AbstractValidator<UpdateJogoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateJogoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome do jogo deve ser informado.")
                .MaximumLength(60).WithMessage("O nome do jogo não deve exceder 60 caracteres.");
        }
    }
}
