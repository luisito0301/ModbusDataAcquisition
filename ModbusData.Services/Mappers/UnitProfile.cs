using AutoMapper;
using ModbusData.Domain.Entities.Unit; // Asegúrate de que la ruta sea correcta
using ModbusData.GrpcProtos; // Asegúrate de que la ruta sea correcta

namespace ModbusData.Services.Mappers
{
    public class UnitProfile : Profile
    {
        public UnitProfile()
        {
            // Mapping from Domain Entity to gRPC DTO
            CreateMap<Domain.Entities.Unit.Unit, UnitDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ManufactererName, opt => opt.MapFrom(src => src.ManufactererName))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.AreaName))
                .ForMember(dest => dest.UnitTypes, opt => opt.MapFrom(src => (int)src.UnitTypes)) // Convert UnitType to int
                .ForMember(dest => dest.Variables, opt => opt.MapFrom(src => src.Variables)); // Asumiendo que Variables es una colección de VariableDTO

            // Mapping from gRPC DTO to Domain Entity
            CreateMap<UnitDTO, Domain.Entities.Unit.Unit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.ManufactererName, opt => opt.MapFrom(src => src.ManufactererName))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.AreaName))
                .ForMember(dest => dest.UnitTypes, opt => opt.MapFrom(src => (UnitType)src.UnitTypes)) // Convert int to UnitType
                .ForMember(dest => dest.Variables, opt => opt.MapFrom(src => src.Variables)); // Asumiendo que Variables es una colección de VariableDTO
        }
    }
}
