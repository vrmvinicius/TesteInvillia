using CleanArchitecture.Application.Amigos.Commands.CreateAmigo;
using CleanArchitecture.Application.Amigos.Commands.DeleteAmigo;
using CleanArchitecture.Application.Amigos.Commands.UpdateAmigo;
using CleanArchitecture.Application.Amigos.Queries.GetAmigos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers
{
    [Authorize]
    public class AmigosController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<AmigoDto>>> GetAmigos([FromQuery] GetAmigosQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateAmigoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateAmigoCommand command)
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
            await Mediator.Send(new DeleteAmigoCommand { Id = id });

            return NoContent();
        }
    }
}
