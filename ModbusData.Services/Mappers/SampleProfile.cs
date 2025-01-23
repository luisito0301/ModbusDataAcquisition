using AutoMapper;
using ModbusData.Domain.Records; 
using ModbusData.GrpcProtos; 

namespace ModbusData.Services.Mappers
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            // Mapeo de la Entidad de Dominio a gRPC DTO
            _=CreateMap<Sample, SampleDTO>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => src.VariableId.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src.Date.ToUniversalTime())))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Mapeo de gRPC DTO a la Entidad de Dominio
            CreateMap<SampleDTO, Sample>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => Guid.Parse(src.VariableId)))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToDateTime()))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Mapeo de NullableSample
            CreateMap<NullableSample, NullableSampleDTO>()
                .ForMember(dest => dest.Sample, opt => opt.MapFrom(src => src.Sample));

            // Mapeo de Samples
            CreateMap<Samples, Samples>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}