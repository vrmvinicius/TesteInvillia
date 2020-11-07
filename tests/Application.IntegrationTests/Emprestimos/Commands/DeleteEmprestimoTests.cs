using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Emprestimos.Commands.CreateEmprestimo;
using CleanArchitecture.Application.Emprestimos.Commands.DeleteEmprestimo;
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

    public class DeleteEmprestimoTests : TestBase
    {
        [Test]
        public void ShouldRequireValidEmprestimoId()
        {
            var command = new DeleteEmprestimoCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteEmprestimo()
        {
            var amigoId = await SendAsync(new CreateAmigoCommand
            {
                Nome = "João da Silva"
            });

            var jogoId = await SendAsync(new CreateJogoCommand
            {
                Nome = "Zelda"
            });

            var emprestimoId = await SendAsync(new CreateEmprestimoCommand
            {
                AmigoId = amigoId,
                JogoId = jogoId
            });

            await SendAsync(new DeleteEmprestimoCommand
            {
                Id = emprestimoId
            });

            var list = await FindAsync<Emprestimo>(emprestimoId);

            list.Should().BeNull();
        }
    }
}
