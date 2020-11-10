using TesteInvillia.Domain.Common;
using TesteInvillia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Domain.Events
{
    public class EmprestimoCompletedEvent : DomainEvent
    {
        public Emprestimo Emprestimo { get; }

        public EmprestimoCompletedEvent(Emprestimo emprestimo)
        {
            Emprestimo = emprestimo;
        }                
    }
}
