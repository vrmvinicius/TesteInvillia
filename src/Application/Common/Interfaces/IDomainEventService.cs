using TesteInvillia.Domain.Common;
using System.Threading.Tasks;

namespace TesteInvillia.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
