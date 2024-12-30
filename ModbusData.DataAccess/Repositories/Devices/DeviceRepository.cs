using ModbusData.Domain.Entities.Device;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Devices;

namespace ModbusData.DataAccess.Repositories.Devices
{
    public class DeviceRepository : RepositoryBase, IDeviceRepository
    {
        public DeviceRepository(ApplicationContext context) : base(context)
        {
        }

        public void AddDevice(SlaveDevice device)
        {
            // Check if the device is already being tracked
            var existingDevice = _context.Set<SlaveDevice>().Local.FirstOrDefault(d => d.Id == device.Id);
            if (existingDevice == null)
            {
                _context.Set<SlaveDevice>().Add(device);
            }
        }

        public void UpdateDevice(SlaveDevice device)
        {
            // Check if the device is already being tracked
            var existingDevice = _context.Set<SlaveDevice>().Local.FirstOrDefault(d => d.Id == device.Id);
            if (existingDevice != null)
            {
                // Update the existing tracked entity
                _context.Entry(existingDevice).CurrentValues.SetValues(device);
            }
            else
            {
                _context.Set<SlaveDevice>().Update(device);
            }
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

       
    }
}
