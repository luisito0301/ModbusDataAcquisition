﻿using ModbusData.Domain.Common;
using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Device
{
    public class SlaveDevice : Entity

    {
     

        public int Id { get; set; }
        public string IpAddress { get; set; }
        public List<Variable> Variables { get; set; } = new List<Variable>();
         public SlaveDevice(int id, string ipAddress, List<Variable> variables)
        {
            Id = id;
            IpAddress = ipAddress;
            Variables = variables;
        }
    }
}
