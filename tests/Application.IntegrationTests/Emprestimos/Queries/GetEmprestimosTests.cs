using CleanArchitecture.Application.Emprestimos.Queries;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Emprestimos.Queries
{
    using static Testing;

    public class GetEmprestimosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllEmprestimos()
        {
            await AddAsync(new Emprestimo
            {
                Amigo = new Amigo() { Nome = "João" },
                Jogo = new Jogo() { Nome = "Counter Strike" },
                Devolvido = false
            });
            await AddAsync(new Emprestimo
            {
                Amigo = new Amigo() { Nome = "Sérgio" },
                Jogo = new Jogo() { Nome = "Gran Turismo" },
                Devolvido = false
            });
            await AddAsync(new Emprestimo
            {
                Amigo = new Amigo() { Nome = "Carlos" },
                Jogo = new Jogo() { Nome = "Formula 1" },
                Devolvido = true
            });

            var query = new GetEmprestimosQuery();

            var result = await SendAsync(query);

            result.Should().HaveCount(2);            
        }
    }
}
