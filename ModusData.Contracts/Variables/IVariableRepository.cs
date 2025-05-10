using ModbusData.Domain.Entities.Variables;
using ModbusData.Domain.Records;
using System;
using System.Collections.Generic;

namespace ModbusData.Contract.Variables
{
    public interface IVariableRepository<T> where T : Variable
    {
        // Métodos existentes
        void Add(T variable);
        T? GetById(Guid id);
        IEnumerable<T> GetAll();
        void Update(T variable);
        void Delete(Guid id);

      
    }
}
