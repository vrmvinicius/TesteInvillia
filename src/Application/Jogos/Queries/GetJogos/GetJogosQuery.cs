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

namespace TesteInvillia.Application.Jogos.Queries.GetJogos
{
    public class GetJogosQuery : IRequest<List<JogoDto>>
    {
    }

    public class GetJogosQueryHandler : IRequestHandler<GetJogosQuery, List<JogoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJogosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<JogoDto>> Handle(GetJogosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Jogos
                .OrderBy(x => x.Nome)
                .ProjectTo<JogoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
