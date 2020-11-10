using TesteInvillia.Application.Emprestimos.Commands.CreateEmprestimo;
using TesteInvillia.Application.Emprestimos.Commands.DeleteEmprestimo;
using TesteInvillia.Application.Emprestimos.Commands.UpdateEmprestimo;
using TesteInvillia.Application.Emprestimos.Queries;
using TesteInvillia.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteInvillia.WebApi.Controllers
{
    [Authorize]    
    public class EmprestimosController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<EmprestimoDto>>> Get()
        {
            return await Mediator.Send(new GetEmprestimosQuery());
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateEmprestimoCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateEmprestimoCommand command)
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
            await Mediator.Send(new DeleteEmprestimoCommand { Id = id });

            return NoContent();
        }
    }
}
