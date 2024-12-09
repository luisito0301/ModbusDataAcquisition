using ModbusData.Domain.Common;
using ModbusData.Domain.Entities.Device;
using ModbusData.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Modbus_Network
{
    public class ModbusNetwork : Entity
    {
        public int Idd { get; set; }
        public IP MasterIpAddress { get; set; }  ///Direccion IP del dispositivo maestro
        public List<SlaveDevice> Slaves { get; set; } = new List<SlaveDevice>();  ///Dispositivos esclavos asociados a la red

        
       
        public ModbusNetwork(int id, IP masterIpAddress, List<SlaveDevice> slaves)
        {
            Idd = id;
            MasterIpAddress = masterIpAddress;
            Slaves = slaves;
        }
        ///<summary>
        ///Requerido por EntityFramework
        ///<summary>
        protected ModbusNetwork() { }
    }
}
