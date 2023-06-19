using System.Numerics;
using System.Runtime.InteropServices;

namespace Forconsol
{
    /// <summary>
    /// Proporciona un par de métodos estáticos para determinar si un tipo predefinido es considerado parte de una clasificación de tipos numéricos. Esta clase no puede ser heredada.
    /// </summary>
    public static class NumberType
    {
        /// <summary>
        /// Determina si un tipo predefinido es un número entero.
        /// </summary>
        /// <param name="tipo">El tipo a comprobar.</param>
        /// <returns><see langword="true"/> si la <see cref="Type"/> es un número entero; en caso contrario, <see langword="false"/>.</returns>
        public static bool IsBinaryInteger(this Type? tipo)
        {
            return tipo == typeof(int) || tipo == typeof(byte) || tipo == typeof(long) || tipo == typeof(char) || tipo == typeof(short) || tipo == typeof(sbyte) || tipo == typeof(ushort) || tipo == typeof(uint) || tipo == typeof(ulong) || tipo == typeof(nint) || tipo == typeof(nuint) || tipo == typeof(BigInteger) || tipo == typeof(Int128) || tipo == typeof(UInt128);
        }

        /// <summary>
        /// Determina si un tipo predefinido es un número.
        /// </summary>
        /// <param name="tipo">El tipo a comprobar.</param>
        /// <returns><see langword="true"/> si la <see cref="Type"/> es un número; en caso contrario, <see langword="false"/>.</returns>
        public static bool IsNumber(this Type? tipo)
        {
            return tipo == typeof(int) || tipo == typeof(double) || tipo == typeof(byte) || tipo == typeof(float) || tipo == typeof(decimal) || tipo == typeof(long) || tipo == typeof(char) || tipo == typeof(short) || tipo == typeof(sbyte) || tipo == typeof(ushort) || tipo == typeof(uint) || tipo == typeof(ulong) || tipo == typeof(nint) || tipo == typeof(nuint) || tipo == typeof(BigInteger) || tipo == typeof(Half) || tipo == typeof(Int128) || tipo == typeof(UInt128) || tipo == typeof(NFloat);
        }        
    }
}
