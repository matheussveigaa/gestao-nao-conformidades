using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GestaoDeNaoConformidades.Application.Queries.ObterTodosDepartamentos;
using GestaoDeNaoConformidades.Rest.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeNaoConformidades.Rest.Controllers
{
    [Route("api/departamento")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DepartamentoController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<DepartamentoDTO[]>> ObterTodosOsDepartamentos()
        {
            var result = await _mediator.Send(new ObterTodosDepartamentosQuery());
            var dtos = _mapper.Map<DepartamentoDTO[]>(result);

            return Ok(dtos);
        }
    }
}