using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.EventHandlers
{
    public class EmprestimoCreateEventHandler
    {
        private readonly ILogger<EmprestimoCompletedEventHandler> _logger;

        public EmprestimoCreateEventHandler(ILogger<EmprestimoCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<EmprestimoCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
