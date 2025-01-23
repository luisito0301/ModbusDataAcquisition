using AutoMapper;
using ModbusData.GrpcProtos;
using ModbusData.Domain.Records;

namespace ModbusData.Services.Mappers
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            // Mapping from Domain Entity to gRPC DTO
            CreateMap<Sample, SampleDTO>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => src.VariableId.ToString())) // Convertir Guid a string
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("o"))) // Convertir a formato ISO 8601
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            // Mapping from gRPC DTO to Domain Entity
            CreateMap<SampleDTO, Sample>()
                .ForMember(dest => dest.VariableId, opt => opt.MapFrom(src => Guid.Parse(src.VariableId))) // Convertir string a Guid
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
        }
    }
}
