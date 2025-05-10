using AutoMapper;
using ModbusData.GrpcProtos;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.ValueObjects;
using System;
using System.Linq;

namespace ModbusData.Services.Mappers
{
    public class ModbusNetworkProfile : Profile
    {
        public ModbusNetworkProfile()
        {
            // Mapping from Domain Entity to gRPC DTO
            CreateMap<ModbusData.Domain.Entities.Modbus_Network.ModbusNetwork, ModbusNetworkDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.MasterIpAddress, opt => opt.MapFrom(src => src.MasterIpAddress.ToString())) // Convert IP to string
                .ForMember(dest => dest.Slaves, opt => opt.MapFrom(src => src.Slaves.ToList()));

            // Mapping from gRPC DTO to Domain Entity
            CreateMap<ModbusNetworkDTO, Domain.Entities.Modbus_Network.ModbusNetwork>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.MasterIpAddress, opt => opt.MapFrom(src => Domain.ValueObjects.IP.Parse(src.MasterIpAddress.ToString()))) // Convert string to IP
                .ForMember(dest => dest.Slaves, opt => opt.MapFrom(src => src.Slaves.ToList()));
        }
    }
}
