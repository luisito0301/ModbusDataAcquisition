using ModbusData.Domain.Entities.Modbus_Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.ModbusNetworks
{
     /// <summary>
    /// Describe las funcionalidades necesarias
    /// para dar persistencia a redes modbus.
    /// </summary>
    public interface IModbusNetworkRepository
    {
         /// <summary>
        /// Añade una red modbus al soporte de datos.
        /// </summary>
        /// <param name="network">Red modbus a añadir.</param>
        void AddModbusNetwork(ModbusNetwork network);
        /// <summary>
        /// Elimina un red modbus del soporte de datos.
        /// </summary>
        /// <param name="network">Red modbus a eliminar.</param>
        void DeleteModbusNetwork(ModbusNetwork network);
        /// <summary>
        /// Obtiene todas las redes modbus del soporte de datos.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModbusNetwork> GetAllModbusNetworks();
        /// <summary>
        /// Obtiene una red modbus del soporte de datos a partir de su identificador.
        /// </summary>
        /// <param name="id">Identificador de la red modbus.</param>
        /// <returns>Red modbus obtenida del soporte de datos; de no existir, <see langword="null"/>.</returns>
        ModbusNetwork? GetModbusNetworkById(Guid id);
        /// <summary>
        /// Actualiza el valor de una red modbus en el soporte de datos.
        /// </summary>
        /// <param name="network">Instancia con la información a actualizar de la red modbus.</param>
        void UpdateModbusNetwork(ModbusNetwork network);
    }

}
