using TesteInvillia.Application.Common.Interfaces;
using System;

namespace TesteInvillia.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
