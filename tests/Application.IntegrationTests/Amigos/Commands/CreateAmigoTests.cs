using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Common.Exceptions;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.IntegrationTests.Amigos.Commands
{
    using static Testing;

    public class CreateAmigoTests : TestBase
    {        
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateAmigoCommand();

            FluentActions.Invoking(() => 
                SendAsync(command)).Should().Throw<ValidationException>();
        }
                
        [Test]
        public async Task ShouldCreateAmigo()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateAmigoCommand
            {
                Nome = "João da Silva"
            };

            var id = await SendAsync(command);

            var amigo = await FindAsync<Amigo>(id);

            amigo.Should().NotBeNull();
            amigo.Nome.Should().Be(command.Nome);
            amigo.CreatedBy.Should().Be(userId);
            amigo.Created.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
