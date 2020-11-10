using TesteInvillia.Application.Amigos.Commands.CreateAmigo;
using TesteInvillia.Application.Common.Exceptions;
using TesteInvillia.Application.Emprestimos.Commands.CreateEmprestimo;
using TesteInvillia.Application.Emprestimos.Commands.DeleteEmprestimo;
using TesteInvillia.Application.Jogos.Commands.CreateJogo;
using TesteInvillia.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TesteInvillia.Application.IntegrationTests.Emrestimos.Commands
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
