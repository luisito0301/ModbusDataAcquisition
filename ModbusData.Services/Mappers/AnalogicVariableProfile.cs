using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Records;
using ModbusData.GrpcProtos;

namespace ModbusData.Services.Mappers
{
    public class VariableProfile : Profile
    {
        public VariableProfile()
        {
            // Mapping from Domain Entity to gRPC DTO for AnalogicVariable
            CreateMap<Domain.Entities.Variables.AnalogicVariable, AnalogicVariableDTO>()
                .ForPath(dest => dest.Base.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForPath(dest => dest.Base.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Base.Type, opt => opt.MapFrom(src => src.Type))
                .ForPath(dest => dest.Base.IsMeasurement, opt => opt.MapFrom(src => src.IsMeasurement))
                .ForPath(dest => dest.Base.Code, opt => opt.MapFrom(src => src.Code))
                .ForPath(dest => dest.Base.SamplingPeriod, opt => opt.MapFrom(src => src.SamplingPeriod.ToString()))
                .ForPath(dest => dest.Base.ModbusAddress, opt => opt.MapFrom(src => src.ModbusAddress))
                .ForPath(dest => dest.Base.Unitid, opt => opt.MapFrom(src => src.UnitId.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForPath(dest => dest.Base.Samples, opt => opt.MapFrom(src => src.Samples));

            // Mapping from gRPC DTO to Domain Entity for AnalogicVariable
            CreateMap<AnalogicVariableDTO, Domain.Entities.Variables.AnalogicVariable>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Base.Id)))
                .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForPath(dest => dest.Type, opt => opt.MapFrom(src => src.Base.Type))
                .ForPath(dest => dest.IsMeasurement, opt => opt.MapFrom(src => src.Base.IsMeasurement))
                .ForPath(dest => dest.Code, opt => opt.MapFrom(src => src.Base.Code))
                .ForPath(dest => dest.SamplingPeriod, opt => opt.MapFrom(src => TimeSpan.Parse(src.Base.SamplingPeriod)))
                .ForPath(dest => dest.ModbusAddress, opt => opt.MapFrom(src => src.Base.ModbusAddress))
                .ForPath(dest => dest.UnitId, opt => opt.MapFrom(src => Guid.Parse(src.Base.Unitid)))
                .ForPath(dest => dest.Samples, opt => opt.MapFrom(src => src.Base.Samples))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Mapping from Domain Entity to gRPC DTO for DigitalVariable
            CreateMap<Domain.Entities.Variables.DigitalVariable, DigitalVariableDTO>()
                .ForPath(dest => dest.Base.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForPath(dest => dest.Base.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Base.Type, opt => opt.MapFrom(src => src.Type))
                .ForPath(dest => dest.Base.IsMeasurement, opt => opt.MapFrom(src => src.IsMeasurement))
                .ForPath(dest => dest.Base.Code, opt => opt.MapFrom(src => src.Code))
                .ForPath(dest => dest.Base.SamplingPeriod, opt => opt.MapFrom(src => src.SamplingPeriod.ToString()))
                .ForPath(dest => dest.Base.ModbusAddress, opt => opt.MapFrom(src => src.ModbusAddress))
                .ForPath(dest => dest.Base.Unitid, opt => opt.MapFrom(src => src.UnitId.ToString()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForPath(dest => dest.Base.Samples, opt => opt.MapFrom(src => src.Samples));

            // Mapping from gRPC DTO to Domain Entity for DigitalVariable
            CreateMap<DigitalVariableDTO, Domain.Entities.Variables.DigitalVariable>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Base.Id)))
                .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Base.Name))
                .ForPath(dest => dest.Type, opt => opt.MapFrom(src => src.Base.Type))
                .ForPath(dest => dest.IsMeasurement, opt => opt.MapFrom(src => src.Base.IsMeasurement))
                .ForPath(dest => dest.Code, opt => opt.MapFrom(src => src.Base.Code))
                .ForPath(dest => dest.SamplingPeriod, opt => opt.MapFrom(src => TimeSpan.Parse(src.Base.SamplingPeriod)))
                .ForPath(dest => dest.ModbusAddress, opt => opt.MapFrom(src => src.Base.ModbusAddress))
                .ForPath(dest => dest.UnitId, opt => opt.MapFrom(src => Guid.Parse(src.Base.Unitid)))
                .ForPath(dest => dest.Samples, opt => opt.MapFrom(src => src.Base.Samples))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Mapping from Domain Entity to gRPC DTO for Sample
            CreateMap<Sample, SampleDTO>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => src.VariableId.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => Timestamp.FromDateTime(src.Date.ToUniversalTime())));

            // Mapping from gRPC DTO to Domain Entity for Sample
            CreateMap<SampleDTO, Sample>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => Guid.Parse(src.VariableId)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToDateTime()));
        }
    }
}
