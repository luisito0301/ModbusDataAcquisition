using ModbusData.Domain.Entities.Modbus_Network;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.ModbusNetworks;

namespace ModbusData.DataAccess.Repositories.ModbusNetworks
{
    public class ModbusNetworkRepository<T> : RepositoryBase<T>, IModbusNetworkRepository<T> where T : ModbusNetwork
    {
        /// <summary>Constructor que inicializa el repositorio con el contexto de la aplicación.</summary>
        /// <param name="context">El contexto de la aplicación.</param>
        public ModbusNetworkRepository(ApplicationContext context) : base(context)
        {
        }

    }
   
    }

