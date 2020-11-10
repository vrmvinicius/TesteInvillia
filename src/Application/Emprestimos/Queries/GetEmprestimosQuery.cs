using AutoMapper;
using AutoMapper.QueryableExtensions;
using TesteInvillia.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Emprestimos.Queries
{
    public class GetEmprestimosQuery : IRequest<List<EmprestimoDto>>
    {
    }

    public class GetEmprestimosQueryHandler : IRequestHandler<GetEmprestimosQuery, List<EmprestimoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmprestimosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmprestimoDto>> Handle(GetEmprestimosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Emprestimos
                                 .Where(e => !e.Devolvido)
                                 .ProjectTo<EmprestimoDto>(_mapper.ConfigurationProvider)
                                 .OrderBy(t => t.Id)
                                 .ToListAsync(cancellationToken);            
        }
    }
}
