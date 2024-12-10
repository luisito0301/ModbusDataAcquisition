using ModbusData.Domain.Entities.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.Devices
{
    public interface IDeviceRepository
    {
        void AddDevice(SlaveDevice device);
        void DeleteDevice(SlaveDevice device);
        IEnumerable<SlaveDevice> GetAllDevices();
        SlaveDevice? GetDeviceById(Guid id);
        void UpdateDevice(SlaveDevice device);
    }

}
