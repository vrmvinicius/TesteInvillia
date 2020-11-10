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

namespace TesteInvillia.Application.Amigos.Queries.GetAmigos
{
    public class GetAmigosQuery : IRequest<List<AmigoDto>>
    {
    }

    public class GetAmigosQueryHandler : IRequestHandler<GetAmigosQuery, List<AmigoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAmigosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AmigoDto>> Handle(GetAmigosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Amigos
                .OrderBy(x => x.Nome)
                .ProjectTo<AmigoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
