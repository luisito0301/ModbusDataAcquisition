using AutoMapper;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Records;
using ModbusData.Domain.Types;
using ModbusData.GrpcProtos;

namespace ModbusData.Services.Mappers
{
    public class VariableProfile : Profile
    {
        public VariableProfile()
        {
            // Mapping from Domain Entity to gRPC DTO for AnalogicVariable
            CreateMap<Domain.Entities.Variables.AnalogicVariable, AnalogicVariableDTO>()
                .ForMember(dest => dest.Base.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Base.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Base.Type, opt => opt.MapFrom(src => (GrpcProtos.VariableType)src.Type))
                .ForMember(dest => dest.Base.IsMeasurement, opt => opt.MapFrom(src => src.IsMeasurement))
                .ForMember(dest => dest.Base.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Base.SamplingPeriod, opt => opt.MapFrom(src => src.SamplingPeriod.ToString()))
                .ForMember(dest => dest.Base.ModbusAddress, opt => opt.MapFrom(src => src.ModbusAddress))
                .ForMember(dest => dest.Base.Unitid, opt => opt.MapFrom(src => src.UnitId.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Base.Samples, opt => opt.MapFrom(src => src.Samples));

            // Mapping from gRPC DTO to Domain Entity for AnalogicVariable
            CreateMap<AnalogicVariableDTO, Domain.Entities.Variables.AnalogicVariable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Base.Id)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Domain.Types.VariableType)src.Base.Type))
                .ForMember(dest => dest.IsMeasurement, opt => opt.MapFrom(src => src.Base.IsMeasurement))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Base.Code))
                .ForMember(dest => dest.SamplingPeriod, opt => opt.MapFrom(src => TimeSpan.Parse(src.Base.SamplingPeriod)))
                .ForMember(dest => dest.ModbusAddress, opt => opt.MapFrom(src => src.Base.ModbusAddress))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => Guid.Parse(src.Base.Unitid)))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Samples, opt => opt.MapFrom(src => src.Base.Samples));

            // Mapping from Domain Entity to gRPC DTO for DigitalVariable
            CreateMap<Domain.Entities.Variables.DigitalVariable, DigitalVariableDTO>()
                .ForMember(dest => dest.Base.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Base.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Base.Type, opt => opt.MapFrom(src => (GrpcProtos.VariableType)src.Type))
                .ForMember(dest => dest.Base.IsMeasurement, opt => opt.MapFrom(src => src.IsMeasurement))
                .ForMember(dest => dest.Base.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Base.SamplingPeriod, opt => opt.MapFrom(src => src.SamplingPeriod.ToString()))
                .ForMember(dest => dest.Base.ModbusAddress, opt => opt.MapFrom(src => src.ModbusAddress))
                .ForMember(dest => dest.Base.Unitid, opt => opt.MapFrom(src => src.UnitId.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Base.Samples, opt => opt.MapFrom(src => src.Samples));

            // Mapping from gRPC DTO to Domain Entity for DigitalVariable
            CreateMap<DigitalVariableDTO, Domain.Entities.Variables.DigitalVariable>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Base.Id)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (Domain.Types.VariableType)src.Base.Type))
                .ForMember(dest => dest.IsMeasurement, opt => opt.MapFrom(src => src.Base.IsMeasurement))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Base.Code))
                .ForMember(dest => dest.SamplingPeriod, opt => opt.MapFrom(src => TimeSpan.Parse(src.Base.SamplingPeriod)))
                .ForMember(dest => dest.ModbusAddress, opt => opt.MapFrom(src => src.Base.ModbusAddress))
                .ForMember(dest => dest.UnitId, opt => opt.MapFrom(src => Guid.Parse(src.Base.Unitid)))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Samples, opt => opt.MapFrom(src => src.Base.Samples));

            // Mapping from Domain Entity to gRPC DTO for Sample
            CreateMap<Sample, SampleDTO>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => src.VariableId.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.Date.ToUniversalTime())));

            // Mapping from gRPC DTO to Domain Entity for Sample
            CreateMap<SampleDTO, Sample>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => Guid.Parse(src.VariableId)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToDateTime()));
        }
    }
}
