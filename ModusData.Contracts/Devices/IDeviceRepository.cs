using ModbusData.Domain.Entities.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.Devices
{
    /// <summary>
    /// Describe las funcionalidades necesarias
    /// para dar persistencia a dispositivos.
    /// </summary>
    public interface IDeviceRepository
    {
        // <summary>
        /// Añade un dispositivo al soporte de datos.
        /// </summary>
        /// <param name="device">Device a añadir.</param>
        void AddDevice(SlaveDevice device);
         /// <summary>
        /// Elimina un dispositivo del soporte de datos
        /// </summary>
        /// <param name="device">Device a eliminar.</param>
        void DeleteDevice(SlaveDevice device);
        /// <summary>
        /// Obtiene todos los dispositivos del soporte de datos.
        /// </summary>
        /// <typeparam name="SlaveDevice"></typeparam>
        /// <returns></returns>
        IEnumerable<SlaveDevice> GetAllDevices();
         /// <summary>
        /// Obtiene un dispositivo del soporte de datos a partir de su identificador.
        /// </summary>
        /// <param name="id">Identificador del Dispositivo.</param>
        /// <returns>CDispositivo obtenido del soporte de datos; de no existir, <see langword="null"/>.</returns>
        SlaveDevice? GetDeviceById(Guid id);
         /// <summary>
        /// Actualiza el valor de un dispositivo en el soporte de datos.
        /// </summary>
        /// <param name="device">Instancia con la información a actualizar del dispositivo.</param>
        void UpdateDevice(SlaveDevice device);
    }

}
