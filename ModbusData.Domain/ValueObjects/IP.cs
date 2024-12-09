using ModbusData.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModbusData.Domain.ValueObjects;

public class IP : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return _octeto1;
        yield return _octeto2;
        yield return _octeto3;
        yield return _octeto4;
    }
    protected IP() { }

    public IP(int octeto1, int octeto2, int octeto3, int octeto4)
    {
        _octeto1 = octeto1;
        _octeto2 = octeto2;
        _octeto3 = octeto3;
        _octeto4 = octeto4;
    }
    public static IP Parse(string ipStr) 
    { var parts = ipStr.Split('.'); 
        return new IP(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])); }

}
