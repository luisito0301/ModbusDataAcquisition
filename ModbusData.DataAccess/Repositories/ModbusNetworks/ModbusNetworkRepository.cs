using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.ModbusNetworks;

namespace ModbusData.DataAccess.Repositories.ModbusNetworks
{
    public class ModbusNetworkRepository : RepositoryBase, IModbusNetworkRepository
    {
        public ModbusNetworkRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddModbusNetwork(ModbusNetwork network)
        {
            // Check if the network is already being tracked
            var existingNetwork = _context.Set<ModbusNetwork>().Local.FirstOrDefault(n => n.Id == network.Id);
            if (existingNetwork == null)
            {
                _context.Set<ModbusNetwork>().Add(network);
            }
        }

        public void UpdateModbusNetwork(ModbusNetwork network)
        {
            // Check if the network is already being tracked
            var existingNetwork = _context.Set<ModbusNetwork>().Local.FirstOrDefault(n => n.Id == network.Id);
            if (existingNetwork != null)
            {
                // Update the existing tracked entity
                _context.Entry(existingNetwork).CurrentValues.SetValues(network);
            }
            else
            {
                _context.Set<ModbusNetwork>().Update(network);
            }
        }

        public void DeleteModbusNetwork(ModbusNetwork network)
        {
            _context.Set<ModbusNetwork>().Remove(network);
        }

        public IEnumerable<ModbusNetwork> GetAllModbusNetworks()
        {
            return _context.Set<ModbusNetwork>().ToList();
        }

        public ModbusNetwork? GetModbusNetworkById(Guid id)
        {
            return _context.Set<ModbusNetwork>().FirstOrDefault(x => x.Id == id);
        }

      
    }
}
