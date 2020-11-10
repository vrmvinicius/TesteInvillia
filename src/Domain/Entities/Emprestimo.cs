using TesteInvillia.Domain.Common;
using TesteInvillia.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Domain.Entities
{
    public class Emprestimo : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }
        public int AmigoId { get; set; }
        public Amigo Amigo { get; set; }
        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }

        private bool _devolvido;
        public bool Devolvido
        {
            get => _devolvido;
            set
            {
                if (value == true && _devolvido == false)
                {
                    DomainEvents.Add(new EmprestimoCompletedEvent(this));
                }

                _devolvido = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
