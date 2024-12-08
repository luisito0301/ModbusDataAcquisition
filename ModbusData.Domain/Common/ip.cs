using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModbusData.Domain.Common;

namespace ModbusData.Domain.Common;

public class IP : ValueObject;
{
    public int _octeto1; 
    public int _octeto2;
    public int _octeto3;
    public int _octeto4;

    public int Octeto1
    {
        get { return _octeto1; }
        set
        {
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(Octeto1), "El valor debe estar entre 0 y 255.");
            _octeto1 = value;
        }
    }

    public int Octeto2
    {
        get { return _octeto2; }
        set
        {
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(Octeto2), "El valor debe estar entre 0 y 255.");
            _octeto2 = value;
        }
    }

    public int Octeto3
    {
        get { return _octeto3; }
        set
        {
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(Octeto3), "El valor debe estar entre 0 y 255.");
            _octeto3 = value;
        }
    }

    public int Octeto4
    {
        get { return _octeto4; }
        set
        {
            if (value < 0 || value > 255)
                throw new ArgumentOutOfRangeException(nameof(Octeto4), "El valor debe estar entre 0 y 255.");
            _octeto4 = value;
        }
    }

    public override string ToString()
    {
        return $"{Octeto1}.{Octeto2}.{Octeto3}.{Octeto4}";
    }
}



