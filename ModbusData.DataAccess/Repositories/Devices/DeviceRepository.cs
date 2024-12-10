using ModbusData.Domain.Entities.Device;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Devices;

namespace ModbusData.DataAccess.Repositories.Devices
{
    /// <summary>
    /// Implementación del repositorio <see cref="IDeviceRepository"/>.
    /// </summary>
    public class DeviceRepository : RepositoryBase, IDeviceRepository
    {
        public DeviceRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddDevice(SlaveDevice device)
        {
            _context.Set<SlaveDevice>().Add(device);
        }

        public void DeleteDevice(SlaveDevice device)
        {
            _context.Set<SlaveDevice>().Remove(device);
        }

        public IEnumerable<SlaveDevice> GetAllDevices()
        {
            return _context.Set<SlaveDevice>().ToList();
        }

        public SlaveDevice? GetDeviceById(Guid id)
        {
            return _context.Set<SlaveDevice>().FirstOrDefault(x => x.Id == id);
        }

        public void UpdateDevice(SlaveDevice device)
        {
            _context.Set<SlaveDevice>().Update(device);
        }
    }
}
