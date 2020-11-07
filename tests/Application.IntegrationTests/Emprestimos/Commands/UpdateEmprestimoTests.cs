using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Emprestimos.Commands.CreateEmprestimo;
using CleanArchitecture.Application.Emprestimos.Commands.UpdateEmprestimo;
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

    public class UpdateEmprestimoTests : TestBase
    {
        [Test]
        public void ShouldRequireValidEmprestimoId()
        {
            var command = new UpdateEmprestimoCommand
            {
                Id = 99
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
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

            var jogoId1 = await SendAsync(new CreateJogoCommand
            {
                Nome = "Zelda"
            });
            var jogoId2 = await SendAsync(new CreateJogoCommand
            {
                Nome = "Full Throttle"
            });

            var emprestimoId1 = await SendAsync(new CreateEmprestimoCommand
            {
                AmigoId = amigoId1,
                JogoId = jogoId1
            });
            await SendAsync(new CreateEmprestimoCommand
            {
                AmigoId = amigoId2,
                JogoId = jogoId2
            });

            var command = new UpdateEmprestimoCommand
            {
                Id = emprestimoId1,
                JogoId = jogoId2
            };

            FluentActions.Invoking(() =>
                SendAsync(command))
                    .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("JogoId"))
                    .And.Errors["JogoId"].Should().Contain("Este jogo já esta emprestado.");
        }

        [Test]
        public async Task ShouldUpdateEmprestimo()
        {
            var userId = await RunAsDefaultUserAsync();

            var amigoId1 = await SendAsync(new CreateAmigoCommand
            {
                Nome = "João da Silva"
            });

            var jogoId1 = await SendAsync(new CreateJogoCommand
            {
                Nome = "Zelda"
            });
            var jogoId2 = await SendAsync(new CreateJogoCommand
            {
                Nome = "Full Throttle"
            });

            var emprestimoId = await SendAsync(new CreateEmprestimoCommand
            {
                AmigoId = amigoId1,
                JogoId = jogoId1
            });

            var command = new UpdateEmprestimoCommand
            {
                Id = emprestimoId,
                JogoId = jogoId2          
            };

            await SendAsync(command);

            var list = await FindAsync<Emprestimo>(emprestimoId);

            list.JogoId.Should().Be(command.JogoId);
            list.LastModifiedBy.Should().NotBeNull();
            list.LastModifiedBy.Should().Be(userId);
            list.LastModified.Should().NotBeNull();
            list.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
