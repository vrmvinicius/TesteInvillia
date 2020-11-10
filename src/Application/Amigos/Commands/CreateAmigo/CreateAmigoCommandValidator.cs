using TesteInvillia.Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Amigos.Commands.CreateAmigo
{
    public class CreateAmigoCommandValidator : AbstractValidator<CreateAmigoCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateAmigoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome deve ser preenchido.")
                .MaximumLength(60).WithMessage("O nome não deve exceder 60 caracteres.");                
        }
    }
}
