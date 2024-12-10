using ModbusData.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.DataAccess.Repositories.Common
{
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Contexto del soporte de datos.
        /// </summary>
        protected ApplicationContext _context;

        protected RepositoryBase(ApplicationContext context)
        {
            _context = context;
        }
    }
}
