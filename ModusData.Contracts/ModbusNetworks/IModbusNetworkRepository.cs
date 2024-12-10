using ModbusData.Domain.Entities.Modbus_Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.ModbusNetworks
{
    public interface IModbusNetworkRepository
    {
        void AddModbusNetwork(ModbusNetwork network);
        void DeleteModbusNetwork(ModbusNetwork network);
        IEnumerable<ModbusNetwork> GetAllModbusNetworks();
        ModbusNetwork? GetModbusNetworkById(Guid id);
        void UpdateModbusNetwork(ModbusNetwork network);
    }

}
