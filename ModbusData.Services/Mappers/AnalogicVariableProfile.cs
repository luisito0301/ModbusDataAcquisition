using AutoMapper;
using ModbusData.Domain.Types;
using ModbusData.GrpcProtos;
namespace ModbusData.Services.Mappers
{
    public class AnalogicVariableProfile : Profile
    {
        public AnalogicVariableProfile()
        {
            // Mapping from Domain Entity to gRPC DTO
            CreateMap<ModbusData.Domain.Entities.Variables.AnalogicVariable, AnalogicVariableDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Domain.Types.VariableType)src.Type)) // Assuming VariableType is an enum
                .ForMember(dest => dest.IsMeasurement, opt => opt.MapFrom(src => src.IsMeasurement))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.SamplingPeriod, opt => opt.MapFrom(src => src.SamplingPeriod.ToString())) // Convert TimeSpan to string
                .ForMember(dest => dest.ModbusAddress, opt => opt.MapFrom(src => src.ModbusAddress))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Mapping from gRPC DTO to Domain Entity
            CreateMap<AnalogicVariableDTO, Domain.Entities.Variables.AnalogicVariable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Domain.Types.VariableType)src.Type)) // Assuming VariableType is an enum
                .ForMember(dest => dest.IsMeasurement, opt => opt.MapFrom(src => src.IsMeasurement))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.SamplingPeriod, opt => opt.MapFrom(src => TimeSpan.Parse(src.SamplingPeriod))) // Convert string to TimeSpan
                .ForMember(dest => dest.ModbusAddress, opt => opt.MapFrom(src => src.ModbusAddress))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
        }
    }
}