using ModbusData.Domain.Common;
using System;
using System.Collections.Generic;

namespace ModbusData.Domain.ValueObjects
{
    /// <summary>
    /// Represents an IP address using four octets.
    /// </summary>
    public class IP : ValueObject
    {
        public int Octeto1 { get; private set; }
        public int Octeto2 { get; private set; }
        public int Octeto3 { get; private set; }
        public int Octeto4 { get; private set; }

        public override string ToString()
        {
            return $"{Octeto1}.{Octeto2}.{Octeto3}.{Octeto4}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Octeto1;
            yield return Octeto2;
            yield return Octeto3;
            yield return Octeto4;
        }

        // Parameterless constructor for EF
        protected IP() { }

        public IP(int octeto1, int octeto2, int octeto3, int octeto4)
        {
            SetOctets(octeto1, octeto2, octeto3, octeto4);
        }

        public static IP Parse(string ipStr)
        {
            var parts = ipStr.Split('.');
            if (parts.Length != 4)
            {
                throw new FormatException("Invalid IP address format.");
            }

            try
            {
                return new IP(
                    int.Parse(parts[0]),
                    int.Parse(parts[1]),
                    int.Parse(parts[2]),
                    int.Parse(parts[3])
                );
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid IP address format.");
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Octet values must be between 0 and 255.");
            }
        }

        private void SetOctets(int octeto1, int octeto2, int octeto3, int octeto4)
        {
            if (octeto1 < 0 || octeto1 > 255) throw new ArgumentOutOfRangeException(nameof(octeto1), "Value must be between 0 and 255.");
            if (octeto2 < 0 || octeto2 > 255) throw new ArgumentOutOfRangeException(nameof(octeto2), "Value must be between 0 and 255.");
            if (octeto3 < 0 || octeto3 > 255) throw new ArgumentOutOfRangeException(nameof(octeto3), "Value must be between 0 and 255.");
            if (octeto4 < 0 || octeto4 > 255) throw new ArgumentOutOfRangeException(nameof(octeto4), "Value must be between 0 and 255.");

            Octeto1 = octeto1;
            Octeto2 = octeto2;
            Octeto3 = octeto3;
            Octeto4 = octeto4;
        }
    }
}