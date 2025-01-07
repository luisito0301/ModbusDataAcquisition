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
            CreateMap<Unit, UnitDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.ManufactererName))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.AreaName))
                .ForMember(dest => dest.Variables, opt => opt.MapFrom(src => src.Variables)); // Assuming Variables is a collection of AnalogicVariableDTO

            // Mapping from gRPC DTO to Domain Entity
            CreateMap<UnitDTO, Unit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.ManufactererName, opt => opt.MapFrom(src => src.ManufacturerName))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.AreaName))
                .ForMember(dest => dest.Variables, opt => opt.MapFrom(src => src.Variables)); // Assuming Variables is a collection of AnalogicVariable
        }
    }
}