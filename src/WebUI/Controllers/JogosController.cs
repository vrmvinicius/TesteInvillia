using CleanArchitecture.Application.Jogos.Commands.CreateJogo;
using CleanArchitecture.Application.Jogos.Commands.DeleteJogo;
using CleanArchitecture.Application.Jogos.Commands.UpdateJogo;
using CleanArchitecture.Application.Jogos.Queries.GetJogos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers
{
    [Authorize]
    public class JogosController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<JogoDto>>> GetAmigos([FromQuery] GetJogosQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateJogoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateJogoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteJogoCommand { Id = id });

            return NoContent();
        }

    }
}
