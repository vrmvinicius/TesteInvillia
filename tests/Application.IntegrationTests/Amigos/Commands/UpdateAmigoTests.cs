using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Amigos.Commands.UpdateAmigo;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Amigos.Commands
{
    using static Testing;

    public class UpdateAmigoTests : TestBase
    {
        [Test]
        public void ShouldRequireValidAmigoId()
        {
            var command = new UpdateAmigoCommand
            {
                Id = 99,
                Nome = "João Teste"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateAmigo()
        {
            var userId = await RunAsDefaultUserAsync();

            var amigoId = await SendAsync(new CreateAmigoCommand
            {
                Nome = "João Novo Nome"
            });

            var command = new UpdateAmigoCommand
            {
                Id = amigoId,
                Nome = "João Nome Atualizado"
            };

            await SendAsync(command);

            var amigo = await FindAsync<Amigo>(amigoId);

            amigo.Nome.Should().Be(command.Nome);
            amigo.LastModifiedBy.Should().NotBeNull();
            amigo.LastModifiedBy.Should().Be(userId);
            amigo.LastModified.Should().NotBeNull();
            amigo.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
