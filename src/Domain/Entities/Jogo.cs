using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Entities
{
    public class Jogo : AuditableEntity
    {
        public int Id { get; set; }
        public String Nome { get; set; }

        public Jogo()
        {

        }
    }
}
