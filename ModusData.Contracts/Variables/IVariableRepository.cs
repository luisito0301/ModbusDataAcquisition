using ModbusData.Domain.Entities.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Contract.Variables
{
  
    public interface IVariableRepository<T> where T : Variable
    {
        /// <summary>Añade una nueva entidad al repositorio.</summary>
        /// <param name="variable">La entidad a añadir.</param>
        void Add(T variable);

        /// <summary>Busca una entidad por su identificador único.</summary>
        /// <param name="id">El identificador único de la entidad.</param>
        /// <returns>La entidad correspondiente al identificador, o null si no se encuentra.</returns>
        T? GetById(Guid id);

        /// <summary>Devuelve todas las entidades del tipo especificado.</summary>
        /// <returns>Una colección de todas las entidades.</returns>
        IEnumerable<T> GetAll();

        /// <summary>Actualiza una entidad existente en el repositorio.</summary>
        /// <param name="variable">La entidad a actualizar.</param>
        void Update(T variable);

        /// <summary>Elimina una entidad del repositorio por su identificador único.</summary>
        /// <param name="id">El identificador único de la entidad a eliminar.</param>
        void Delete(Guid id);
    }
}
