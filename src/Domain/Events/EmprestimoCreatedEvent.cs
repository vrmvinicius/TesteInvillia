using TesteInvillia.Domain.Common;
using TesteInvillia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Domain.Events
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
