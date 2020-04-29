using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using GestaoDeNaoConformidades.Application.Commands.InserirNaoConformidade;
using GestaoDeNaoConformidades.Application.Queries.ObterTodasNaoConformidades;
using GestaoDeNaoConformidades.Rest.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeNaoConformidades.Rest.Controllers
{
    [Route("api/nao-conformidade")]
    public class NaoConformidadeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public NaoConformidadeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SalvarNaoConformidade([FromBody] NaoConformidadeDTO dto)
        {
            var command = _mapper.Map<InserirNaoConformidadeCommand>(dto);

            var validator = new InserirNaoConformidadeCommandValidator();

            await validator.ValidateAndThrowAsync(command);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<NaoConformidadeDTO[]>> ObterTodasNaoConformidades()
        {
            var query = new ObterTodasNaoConformidadesQuery();

            var result = await _mediator.Send(query);
            var dtos = _mapper.Map<NaoConformidadeDTO[]>(result);

            return Ok(dtos);
        }
    }
}
