using TesteInvillia.Application.Amigos.Commands.CreateAmigo;
using TesteInvillia.Application.Amigos.Commands.DeleteAmigo;
using TesteInvillia.Application.Common.Exceptions;
using TesteInvillia.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteInvillia.Application.IntegrationTests.Amigos.Commands
{
    using static Testing;

    public class DeleteAmigoTests : TestBase
    {
        [Test]
        public void ShouldRequireValidAmigoId()
        {
            var command = new DeleteAmigoCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteAmigo()
        {
            var amigoId = await SendAsync(new CreateAmigoCommand
            {
                Nome = "Carlos Antônio"
            });

            await SendAsync(new DeleteAmigoCommand
            {
                Id = amigoId
            });

            var amigo = await FindAsync<Amigo>(amigoId);

            amigo.Should().BeNull();
        }
    }
}
