using ModbusData.Domain.Common;
using ModbusData.Domain.Records;
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
        public int Id { get; set; } ///Identificador de la variable
        public string Name { get; set; }  ///Nombre de la variable
        public VariableType Type { get; set; }  /// Tipo: Analogica o Digital
        public bool IsMeasurement { get; set; } /// true si es medición, false si es acción de control
        public string Code { get; set; }  ///Codigo de la variable
        TimeSpan SamplingPeriod { get; set; } /// Intervalo de tiempo
        public int ModbusAddress { get; set; }  ///Direccion en el protocolo ModBus
        public Variable(int id, string name, VariableType type, bool isMeasurement, string code, TimeSpan samplingPeriod, int modbusAddress)
        {
            ; 
            Id = id;  
            Type = type;  
            IsMeasurement = isMeasurement; 
            Code = code;
            SamplingPeriod = samplingPeriod;
            ModbusAddress = modbusAddress;
        }
        
    }
    
    }
   
    
    
   



