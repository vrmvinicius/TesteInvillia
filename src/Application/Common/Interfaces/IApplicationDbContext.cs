using TesteInvillia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {        
        DbSet<Amigo> Amigos { get; set; }
        DbSet<Jogo> Jogos { get; set; }
        DbSet<Emprestimo> Emprestimos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
