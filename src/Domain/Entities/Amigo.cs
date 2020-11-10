using TesteInvillia.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteInvillia.Domain.Entities
{
    public class Amigo : AuditableEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
    }
}
