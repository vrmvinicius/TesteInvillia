using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Amigos.Commands.DeleteAmigo;
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
