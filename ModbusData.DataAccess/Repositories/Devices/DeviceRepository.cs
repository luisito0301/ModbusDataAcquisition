using ModbusData.Domain.Entities.Device;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Devices;

namespace ModbusData.DataAccess.Repositories.Devices
{
    public class DeviceRepository<T> : RepositoryBase<T>, IDeviceRepository<T> where T : SlaveDevice
    {
        /// <summary>Constructor que inicializa el repositorio con el contexto de la aplicación.</summary>
        /// <param name="context">El contexto de la aplicación.</param>
        public DeviceRepository(ApplicationContext context) : base(context)
        {
        }

    }
    
       
    }

