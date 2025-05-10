using ModbusData.Domain.Entities.Variables;
using ModbusData.DataAccess.Contexts;
using ModbusData.DataAccess.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using ModbusData.Contract.Variables;
using Microsoft.EntityFrameworkCore;
using ModbusData.Domain.Records;

namespace ModbusData.DataAccess.Repositories.Variables
{
    public class VariableRepository<T> : RepositoryBase<T>, Contract.Variables.IVariableRepository<T> where T : Variable
    {
        /// <summary>Constructor que inicializa el repositorio con el contexto de la aplicación.</summary>
        /// <param name="context">El contexto de la aplicación.</param>
        public VariableRepository(ApplicationContext context) : base(context)
        {
        }

       
    }
}
