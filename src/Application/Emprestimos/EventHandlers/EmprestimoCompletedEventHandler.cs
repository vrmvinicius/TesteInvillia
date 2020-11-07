﻿using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Emprestimos.EventHandlers
{
    public class EmprestimoCompletedEventHandler : INotificationHandler<DomainEventNotification<EmprestimoCompletedEvent>>
    {
        private readonly ILogger<EmprestimoCompletedEventHandler> _logger;

        public EmprestimoCompletedEventHandler(ILogger<EmprestimoCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<EmprestimoCompletedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
