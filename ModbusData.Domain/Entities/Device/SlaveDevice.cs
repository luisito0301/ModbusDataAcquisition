using ModbusData.Domain.Common;
using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace ModbusData.Domain.Entities.Device
{
    public class SlaveDevice : Entity
    {
        public IP IpAddress { get; set; }  // IP address of the device
        public List<Variable> Variables { get; set; } = new List<Variable>();  // Associated variables

 

        public SlaveDevice(Guid id, IP ipAddress, List<Variable> variables) : base(id)
        {
            IpAddress = ipAddress ?? throw new ArgumentNullException(nameof(ipAddress), "IP address cannot be null.");
            Variables = variables ?? new List<Variable>(); // Initialize if null
        }

        // Required by Entity Framework
        protected SlaveDevice() { }
    }
}