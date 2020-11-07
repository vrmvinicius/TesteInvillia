using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Emprestimos.Commands.CreateEmprestimo;
using CleanArchitecture.Application.Jogos.Commands.CreateJogo;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.IntegrationTests.Emrestimos.Commands
{
    using static Testing;

    public class CreateEmprestimoTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateEmprestimoCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldRequireJogoDisponivel()
        {
            var amigoId1 = await SendAsync(new CreateAmigoCommand
            {
                Nome = "João da Silva"
            });
            var amigoId2 = await SendAsync(new CreateAmigoCommand
            {
                Nome = "Carlos Alberto"
            });

            var jogoId = await SendAsync(new CreateJogoCommand
            {
                Nome = "Zelda"
            });

            await SendAsync(new CreateEmprestimoCommand
            {
                AmigoId = amigoId1,
                JogoId = jogoId
            });;

            var command = new CreateEmprestimoCommand
            {
                AmigoId = amigoId2,
                JogoId = jogoId
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateEmprestimo()
        {
            var userId = await RunAsDefaultUserAsync();

            var amigoId = await SendAsync(new CreateAmigoCommand
            {
                Nome = "João da Silva"
            });
            var jogoId = await SendAsync(new CreateJogoCommand
            {
                Nome = "Zelda"
            });

            var command = new CreateEmprestimoCommand
            {
                AmigoId = amigoId,
                JogoId = jogoId                
            };

            var emprestimoId = await SendAsync(command);
            var emprestimo = await FindAsync<Emprestimo>(emprestimoId);

            emprestimo.Should().NotBeNull();
            emprestimo.AmigoId.Should().Be(command.AmigoId);
            emprestimo.JogoId.Should().Be(command.JogoId);                        
            emprestimo.CreatedBy.Should().Be(userId);
            emprestimo.Created.Should().BeCloseTo(DateTime.Now, 10000);
            emprestimo.LastModifiedBy.Should().BeNull();
            emprestimo.LastModified.Should().BeNull();
        }
    }
}
