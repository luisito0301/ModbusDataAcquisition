using ModbusData.Domain.Common;
using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Device
{
    public class SlaveDevice : Entity
    {
        public IP IpAddress { get; set; }  ///Direccion IP del dispositivo
        public List<Variable> Variables { get; set; } = new List<Variable>();  ///Variables asociadas al dispositivo
         public SlaveDevice(Guid id, IP ipAddress, List<Variable> variables):base(id)
        {
           
            IpAddress = ipAddress;
            Variables = variables;
        }
        ///<summary>
        ///Requerido por EntityFramework
        ///<summary>
        protected SlaveDevice() { }
    }
}
