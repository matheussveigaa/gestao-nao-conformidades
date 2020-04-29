using GestaoDeNaoConformidades.Application.ViewModels;
using GestaoDeNaoConformidades.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Application.Queries.ObterTodosDepartamentos
{
    public class ObterTodosDepartamentosQuery : IRequest<DepartamentoViewModel[]>
    {

    }
}
