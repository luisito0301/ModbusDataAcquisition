using ModbusData.Domain.Common;
using ModbusData.Domain.Entities.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Modbus_Network
{
    public class ModbusNetwork : Entity
    {
        public int Id { get; set; }
        public string MasterIpAddress { get; set; }  ///Direccion IP del dispositivo maestro
        public List<SlaveDevice> Slaves { get; set; } = new List<SlaveDevice>();  ///Dispositivos esclavos asociados a la red

        
       
        public ModbusNetwork(int id, string masterIpAddress, List<SlaveDevice> slaves)
        {
            Id = id;
            MasterIpAddress = masterIpAddress;
            Slaves = slaves;
        }
    }
}
