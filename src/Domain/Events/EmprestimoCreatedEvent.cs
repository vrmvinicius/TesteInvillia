using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Events
{
    public class EmprestimoCreatedEvent : DomainEvent
    {
        public Emprestimo Emprestimo { get; }

        public EmprestimoCreatedEvent(Emprestimo emprestimo)
        {
            Emprestimo = emprestimo;
        }
    }
}
