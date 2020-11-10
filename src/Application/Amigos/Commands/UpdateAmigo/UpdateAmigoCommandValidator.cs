using TesteInvillia.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Amigos.Commands.UpdateAmigo
{
    public class UpdateAmigoCommandValidator : AbstractValidator<UpdateAmigoCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAmigoCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Nome)
                .NotEmpty().WithMessage("O nome deve ser informado.")
                .MaximumLength(60).WithMessage("O nome não deve exceder 60 caracteres.");                
        }
    }
}
