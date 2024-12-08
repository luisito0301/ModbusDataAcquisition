using ModbusData.Domain.Common;
using ModbusData.Domain.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusData.Domain.Entities.Variables
{


    public abstract class Variable : Entity
    {
     

        

        public int Id { get; set; } 
        public string Name { get; set; }
        public VariableType Type { get; set; }
        public bool IsMeasurement { get; set; } // true si es medición, false si es acción de control
        public string Code { get; set; }
        TimeSpan SamplingPeriod { get; set; } // Intervalo de tiempo
        public int ModbusAddress { get; set; }
        public Variable(int id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
        {
            Id = id;
            Name = name;
            Type = type;
            IsMeasurement = isMeasurement;
            Code = code;
            SamplingPeriod = samplingPeriod;
            ModbusAddress = modbusAddress;
        }

        // Método para obtener valor de muestra
        public virtual decimal GetSample()
        {
            throw new NotImplementedException("Este método debe ser implementado en las subclases.");
        }
    }
    
    }
   
    
    
   



