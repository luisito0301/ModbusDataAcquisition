using AutoMapper;
using Google.Protobuf.Collections;
using ModbusData.GrpcProtos;
using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModbusData.Services.Mappers
{
    public class SlaveDeviceProfile : Profile
    {
        public SlaveDeviceProfile()
        {
            

            // Mapping from Domain Entity to gRPC DTO
            CreateMap<ModbusData.Domain.Entities.Device.SlaveDevice, SlaveDeviceDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.Variables, opt => opt.MapFrom(src => src.Variables.ToList()));

            // Mapping from gRPC DTO to Domain Entity
            CreateMap<SlaveDeviceDTO, ModbusData.Domain.Entities.Device.SlaveDevice>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(dest => dest.IpAddress, opt => opt.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.Variables, opt => opt.MapFrom(src => src.Variables.ToList()));
        }
    }

   
    }
