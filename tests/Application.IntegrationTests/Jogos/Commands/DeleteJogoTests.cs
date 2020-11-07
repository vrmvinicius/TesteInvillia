using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Jogos.Commands.CreateJogo;
using CleanArchitecture.Application.Jogos.Commands.DeleteJogo;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Jogos.Commands
{
    using static Testing;

    public class DeleteJogoTests : TestBase
    {
        [Test]
        public void ShouldRequireValidJogoId()
        {
            var command = new DeleteJogoCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteJogo()
        {
            var jogoId = await SendAsync(new CreateJogoCommand
            {
                Nome = "Super Mário Brós"
            });

            await SendAsync(new DeleteJogoCommand
            {
                Id = jogoId
            });

            var jogo = await FindAsync<Jogo>(jogoId);

            jogo.Should().BeNull();
        }
    }
}
