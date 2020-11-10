using TesteInvillia.Application.Common.Exceptions;
using TesteInvillia.Application.Jogos.Commands.CreateJogo;
using TesteInvillia.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteInvillia.Application.IntegrationTests.Jogos.Commands
{
    using static Testing;

    public class CreateJogoTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateJogoCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateJogo()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateJogoCommand
            {
                Nome = "River Raid"
            };

            var id = await SendAsync(command);

            var amigo = await FindAsync<Jogo>(id);

            amigo.Should().NotBeNull();
            amigo.Nome.Should().Be(command.Nome);
            amigo.CreatedBy.Should().Be(userId);
            amigo.Created.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
