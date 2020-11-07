using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Jogos.Commands.CreateJogo;
using CleanArchitecture.Application.Jogos.Commands.UpdateJogo;
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

    public class UpdateJogoTests : TestBase
    {
        [Test]
        public void ShouldRequireValidJogoId()
        {
            var command = new UpdateJogoCommand
            {
                Id = 99,
                Nome = "Jogo Teste"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateJogo()
        {
            var userId = await RunAsDefaultUserAsync();

            var jogoId = await SendAsync(new CreateJogoCommand
            {
                Nome = "Jogo Novo"
            });

            var command = new UpdateJogoCommand
            {
                Id = jogoId,
                Nome = "Jogo Novo Atualizado"
            };

            await SendAsync(command);

            var jogo = await FindAsync<Jogo>(jogoId);

            jogo.Nome.Should().Be(command.Nome);
            jogo.LastModifiedBy.Should().NotBeNull();
            jogo.LastModifiedBy.Should().Be(userId);
            jogo.LastModified.Should().NotBeNull();
            jogo.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
