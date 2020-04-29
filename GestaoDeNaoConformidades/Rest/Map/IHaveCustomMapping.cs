using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Rest.Map
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
