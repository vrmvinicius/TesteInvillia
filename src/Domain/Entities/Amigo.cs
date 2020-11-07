using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    public class Amigo : AuditableEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
    }
}
