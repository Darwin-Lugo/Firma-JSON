using AutoMapper;
using Firma.Core.DTOs;
using Firma.Core.Entitys;

namespace Firma.Insfrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PersonaDTO, Persona>().ReverseMap();
        }
    }
}
