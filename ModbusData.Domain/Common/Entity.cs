using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Common
{
    /// <summary>
    /// Clase base para todas las entidades en el soporte de datos.
    /// </summary>
    public abstract class Entity
    {
        #region Properties
         /// <summary>
        /// Identificador en el soporte de datos.
        /// </summary>
       public Guid Id { get; set; }
           #endregion
        /// <summary>
        /// Requerido por EntityFramework.
        /// </summary>
        protected Entity() { }
         /// <summary>
        /// <param name="id">Identificador de la entidad.</param>
        /// </summary>
        protected Entity(Guid id)
        {
            Id = id;
          }

    }
}
