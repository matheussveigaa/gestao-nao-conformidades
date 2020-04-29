using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestaoDeNaoConformidades.Application.Commands.InserirAcaoCorretiva;
using GestaoDeNaoConformidades.Rest.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeNaoConformidades.Rest.Controllers
{
    [Route("api/acao-corretiva")]
    public class AcaoCorretivaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AcaoCorretivaController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SalvarAcaoCorretiva([FromBody] AcaoCorretivaDTO dto)
        {
            var command = _mapper.Map<InserirAcaoCorretivaCommand>(dto);

            await _mediator.Send(command);

            return Ok();
        }
    }
}