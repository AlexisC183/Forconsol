using System.Numerics;
using System.Text.RegularExpressions;

namespace Forconsol
{
    /// <summary>
    /// Representa un número racional expresado en forma fraccionaria. Al crear una instancia de <see cref="Fraccion"/> puede que se efectúe un proceso de simplificación.
    /// </summary>
    public readonly struct Fraccion : IComparable, IComparable<Fraccion>, IConvertibleADecimal<Fraccion>, IEquatable<Fraccion>, IAdditionOperators<Fraccion, Fraccion, Fraccion>, IComparisonOperators<Fraccion, Fraccion, bool>, IDecrementOperators<Fraccion>, IDivisionOperators<Fraccion, Fraccion, Fraccion>, IEqualityOperators<Fraccion, Fraccion, bool>, IIncrementOperators<Fraccion>, IMinMaxValue<Fraccion>, IModulusOperators<Fraccion, Fraccion, Fraccion>, IMultiplyOperators<Fraccion, Fraccion, Fraccion>, ISubtractionOperators<Fraccion, Fraccion, Fraccion>, IUnaryNegationOperators<Fraccion, Fraccion>, IUnaryPlusOperators<Fraccion, Fraccion>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la estructura <see cref="Fraccion"/> al numerador y denominador especificados.
        /// </summary>
        /// <param name="numerador">El numerador.</param>
        /// <param name="denominador">El denominador (diferente que 0).</param>
        /// <exception cref="DivideByZeroException"></exception>
        public Fraccion(int numerador, int denominador)
        {
            if (denominador == 0)
            {
                throw new DivideByZeroException();
            }
            this.numerador = numerador;
            this.denominador = denominador;

            NumeroRacional.GetNumeradorYDenominador(this.numerador / (decimal)this.denominador, out this.numerador, out this.denominador);

            if (denominador != int.MinValue && this.denominador > Math.Abs(denominador))
            {
                this.numerador = numerador;
                this.denominador = denominador;
            }
            if (this.denominador != 1)
            {
                bool hasSigno = false;

                if (int.IsNegative(this.numerador) ^ int.IsNegative(this.denominador))
                {
                    hasSigno = true;
                }
                this.numerador = Math.Abs(this.numerador);
                this.denominador = Math.Abs(this.denominador);

                //Esta condición es por motivos de complejidad temporal
                if (this.denominador < 100000 || this.numerador < 10110)
                {
                    NumeroPrimo numeroPrimo = new();

                    int divisorPrimo = numeroPrimo.Next();

                    while (divisorPrimo <= this.numerador && divisorPrimo <= this.denominador)
                    {
                        if (this.numerador % divisorPrimo == 0 && this.denominador % divisorPrimo == 0)
                        {
                            this.numerador /= divisorPrimo;
                            this.denominador /= divisorPrimo;
                            numeroPrimo.Reset();
                        }
                        divisorPrimo = numeroPrimo.Next();
                    }
                }
                if (hasSigno)
                {
                    this.numerador *= -1;
                }
            }
        }

        private readonly int denominador, numerador;
        private static Regex expresionRegular = new(@"^\s*-?\d+\s*/\s*-?\d+\s*$");

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa cero enteros (0 / 1).
        /// </summary>
        /// <returns>Un objeto cuyo valor es cero enteros.</returns>
        public static Fraccion Cero { get => new(); }

        /// <summary>
        /// Obtiene el denominador representado por esta instancia.
        /// </summary>
        /// <returns>El denominador, en su forma simplificada si fue posible.</returns>
        public int Denominador { get => (denominador == 0) ? 1 : denominador; }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa 1 / 1000000000.
        /// </summary>
        /// <returns>Un objeto cuyo valor es el número mayor que cero más pequeño que puede ser representado por una <see cref="Fraccion"/>.</returns>
        public static Fraccion Epsilon { get => new(1, 1000000000); }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa 2147483647 / 1.
        /// </summary>
        /// <returns>Un objeto cuyo valor es el número más grande que puede ser representado por una <see cref="Fraccion"/>.</returns>
        public static Fraccion MaxValue { get => int.MaxValue; }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa -2147483648 / 1.
        /// </summary>
        /// <returns>Un objeto cuyo valor es el número más pequeño que puede ser representado por una <see cref="Fraccion"/>.</returns>
        public static Fraccion MinValue { get => int.MinValue; }

        /// <summary>
        /// Obtiene el numerador representado por esta instancia.
        /// </summary>
        /// <returns>El numerador, en su forma simplificada si fue posible.</returns>
        public int Numerador { get => numerador; }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa un cuarto (1 / 4).
        /// </summary>
        /// <returns>Un objeto cuyo valor es un cuarto.</returns>
        public static Fraccion UnCuarto { get => new(1, 4); }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa un entero (1 / 1).
        /// </summary>
        /// <returns>Un objeto cuyo valor es un entero.</returns>
        public static Fraccion UnEntero { get => 1; }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa un medio (1 / 2).
        /// </summary>
        /// <returns>Un objeto cuyo valor es un medio.</returns>
        public static Fraccion UnMedio { get => new(1, 2); }

        /// <summary>
        /// Obtiene un objeto <see cref="Fraccion"/> que representa un tercio (1 / 3).
        /// </summary>
        /// <returns>Un objeto cuyo valor es un tercio.</returns>
        public static Fraccion UnTercio { get => new(1, 3); }

        /// <summary>
        /// Calcula el absoluto de un valor.
        /// </summary>
        /// <param name="fraccion">La fracción para la cual obtener su absoluto.</param>
        /// <returns>
        /// <para>
        /// El absoluto de
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        /// <exception cref="OverflowException"></exception>
        public static Fraccion Abs(Fraccion fraccion)
        {
            return IsNegative(fraccion) ? checked(-fraccion) : fraccion;
        }

        /// <summary>
        /// Compara esta instancia con una fracción especificada y devuelve una indicación de sus valores relativos.
        /// </summary>
        /// <param name="fraccion">Una fracción a comparar.</param>
        /// <returns>
        /// Un número con signo indicando los valores relativos de esta instancia y <paramref name="fraccion"/>.
        /// <list>
        /// <listheader>
        /// <term>Valor devuelto</term>
        /// <description>Descripción</description>
        /// </listheader>
        /// <item>
        /// <term>Menor que cero</term>
        /// <description>Esta instancia es menor que <paramref name="fraccion"/>.</description>
        /// </item>
        /// <item>
        /// <term>Cero</term>
        /// <description>Esta instancia es igual a <paramref name="fraccion"/>.</description>
        /// </item>
        /// <item>
        /// <term>Mayor que cero</term>
        /// <description>Esta instancia es mayor que <paramref name="fraccion"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        public int CompareTo(Fraccion fraccion)
        {
            return ((decimal)this).CompareTo((decimal)fraccion);
        }

        /// <summary>
        /// Compara esta instancia con un objeto especificado y devuelve una indicación de sus valores relativos.
        /// </summary>
        /// <param name="objeto">Un objeto a comparar, o null.</param>
        /// <returns>
        /// Un número con signo indicando los valores relativos de esta instancia y <paramref name="objeto"/>.
        /// <list>
        /// <listheader>
        /// <term>Valor devuelto</term>
        /// <description>Descripción</description>
        /// </listheader>
        /// <item>
        /// <term>Menor que cero</term>
        /// <description>Esta instancia es menor que <paramref name="objeto"/>.</description>
        /// </item>
        /// <item>
        /// <term>Cero</term>
        /// <description>Esta instancia es igual a <paramref name="objeto"/>.</description>
        /// </item>
        /// <item>
        /// <term>Mayor que cero</term>
        /// <description>Esta instancia es mayor que <paramref name="objeto"/>, u <paramref name="objeto"/> es <see langword="null"/>.</description>
        /// </item>
        /// </list>
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object? objeto)
        {
            if (objeto is Fraccion)
            {
                return CompareTo((Fraccion)objeto);
            }
            else if (objeto == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Devuelve un valor que indica si la instancia actual y un objeto <see cref="Fraccion"/> especificado tienen el mismo valor.
        /// </summary>
        /// <param name="fraccion">El objeto a comparar.</param>
        /// <returns><see langword="true"/> si este objeto <see cref="Fraccion"/> y <paramref name="fraccion"/> tienen el mismo valor; en caso contrario, <see langword="false"/>.</returns>
        public bool Equals(Fraccion fraccion)
        {
            return this == fraccion;
        }

        /// <summary>
        /// Devuelve un valor que indica si la instancia actual y un objeto especificado tienen el mismo valor.
        /// </summary>
        /// <param name="objeto">El objeto a comparar.</param>
        /// <returns><see langword="true"/> si el argumento <paramref name="objeto"/> es un objeto <see cref="Fraccion"/>, y su valor es igual al valor de la instancia <see cref="Fraccion"/> actual; en caso contrario, <see langword="false"/>.</returns>
        public override bool Equals(object? objeto)
        {
            return objeto is Fraccion && this == (Fraccion)objeto;
        }

        /// <summary>
        /// Devuelve el código hash para el objeto <see cref="Fraccion"/> actual.
        /// </summary>
        /// <returns>Un código hash de tipo int.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerador, Denominador);
        }

        /// <summary>
        /// Determina si un valor representa un número entero par.
        /// </summary>
        /// <param name="fraccion">El valor a comprobar.</param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es un entero par; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool IsEvenInteger(Fraccion fraccion)
        {
            return decimal.IsEvenInteger((decimal)fraccion);
        }

        /// <summary>
        /// Determina si un valor representa un valor entero.
        /// </summary>
        /// <param name="fraccion">El valor a comprobar.</param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es un entero; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool IsInteger(Fraccion fraccion)
        {
            return fraccion.Denominador == 1;
        }

        /// <summary>
        /// Determina si el valor especificado es negativo.
        /// </summary>
        /// <param name="fraccion">Una fracción.</param>
        /// <returns><see langword="true"/> si el valor es negativo; <see langword="false"/> en caso contrario.</returns>
        public static bool IsNegative(Fraccion fraccion)
        {
            return fraccion < Cero;
        }

        /// <summary>
        /// Determina si un valor representa un número entero impar.
        /// </summary>
        /// <param name="fraccion">El valor a comprobar.</param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es un entero impar; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool IsOddInteger(Fraccion fraccion)
        {
            return !IsEvenInteger(fraccion);
        }

        /// <summary>
        /// Determina si un valor es positivo.
        /// </summary>
        /// <param name="fraccion">El valor a comprobar.</param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es positivo; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool IsPositive(Fraccion fraccion)
        {
            return !IsNegative(fraccion);
        }

        /// <summary>
        /// Convierte la representación en cadena de una fracción a su equivalente en fracción.
        /// </summary>
        /// <param name="cadenaFraccion">Una cadena que contiene una fracción a convertir.</param>
        /// <returns>Una fracción que es equivalente a la fracción especificada en <paramref name="cadenaFraccion"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static Fraccion Parse(string cadenaFraccion)
        {
            try
            {
                return int.Parse(cadenaFraccion);
            }
            catch (FormatException)
            {
                Match coincidencia = expresionRegular.Match(cadenaFraccion);

                if (coincidencia.Success)
                {
                    return new(int.Parse(cadenaFraccion.Remove(cadenaFraccion.IndexOf('/'))), int.Parse(cadenaFraccion.Substring(cadenaFraccion.IndexOf('/') + 1)));
                }
                else
                {
                    throw new FormatException();
                }
            }
        }

        /// <summary>
        /// Calcula el recíproco de un valor.
        /// </summary>
        /// <param name="fraccion">La fracción para la cual obtener su recíproco.</param>
        /// <returns>
        /// <para>
        /// El recíproco de
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        /// <exception cref="DivideByZeroException"></exception>
        public static Fraccion Reciproco(Fraccion fraccion)
        {
            return new(fraccion.Denominador, fraccion.Numerador);
        }

        /// <summary>
        /// Convierte la fracción de esta instancia a su representación en cadena equivalente.
        /// </summary>
        /// <returns>La representación en cadena del valor de esta instancia.</returns>
        public override string ToString()
        {
            return Numerador + " / " + Denominador;
        }

        /// <summary>
        /// Convierte la representación en cadena de una fracción a su fracción equivalente. Un valor devuelto indica si la conversión tuvo éxito o falló.
        /// </summary>
        /// <param name="cadenaFraccion">Una cadena que contiene una fracción a convertir.</param>
        /// <param name="resultado">Cuando este método regresa, contiene a la fracción equivalente del parámetro cadenaFraccion, si la conversión tuvo éxito, o cero si la conversión falló. La conversión falla si el parámetro cadenaFraccion es null, no tiene el formato correcto, el numerador o el denominador desbordan el tipo int o el denominador es cero. Este parámetro pasa sin inicializar; cualquier valor suministrado originalmente en resultado será sobrescrito.</param>
        /// <returns><see langword="true"/> si <paramref name="cadenaFraccion"/> se convirtió con éxito; en caso contrario, <see langword="false"/>.</returns>
        public static bool TryParse(string? cadenaFraccion, out Fraccion resultado)
        {
            try
            {
                try
                {
                    resultado = int.Parse(cadenaFraccion);
                    return true;
                }
                catch (FormatException)
                {
                    Match coincidencia = expresionRegular.Match(cadenaFraccion);

                    if (coincidencia.Success)
                    {
                        resultado = new(int.Parse(cadenaFraccion.Remove(cadenaFraccion.IndexOf('/'))), int.Parse(cadenaFraccion.Substring(cadenaFraccion.IndexOf('/') + 1)));
                        return true;
                    }
                }
            }
            catch (SystemException)
            {

            }
            resultado = Cero;
            return false;
        }

        /// <summary>
        /// Suma dos valores juntos para calcular su suma.
        /// </summary>
        /// <param name="sumando"></param>
        /// <param name="sumando1"></param>
        /// <returns>
        /// <para>
        /// La suma de
        /// </para>
        /// <para>
        /// <c>sumando</c>
        /// </para>
        /// <para>
        /// y
        /// </para>
        /// <para>
        /// <c>sumando1</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator +(Fraccion sumando, Fraccion sumando1)
        {
            try
            {
                return ((decimal)sumando + (decimal)sumando1).ToFraccion();
            }
            catch (OverflowException)
            {
                return new(sumando.Numerador * sumando1.Denominador + sumando.Denominador * sumando1.Numerador, sumando.Denominador * sumando1.Denominador);
            }
        }

        /// <summary>
        /// Suma dos valores juntos para calcular su suma.
        /// </summary>
        /// <param name="sumando"></param>
        /// <param name="sumando1"></param>
        /// <returns>
        /// <para>
        /// La suma de
        /// </para>
        /// <para>
        /// <c>sumando</c>
        /// </para>
        /// <para>
        /// y
        /// </para>
        /// <para>
        /// <c>sumando1</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked +(Fraccion sumando, Fraccion sumando1)
        {
            return ((decimal)sumando + (decimal)sumando1).ToFraccion();
        }

        /// <summary>
        /// Decrementa un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// El resultado de decrementar
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked --(Fraccion fraccion)
        {
            checked
            {
                return fraccion - UnEntero;
            }
        }

        /// <summary>
        /// Divide dos valores juntos para calcular su cociente.
        /// </summary>
        /// <param name="dividendo"></param>
        /// <param name="divisor"></param>
        /// <returns>
        /// <para>
        /// El cociente de
        /// </para>
        /// <para>
        /// <c>dividendo</c>
        /// </para>
        /// <para>
        /// dividido por
        /// </para>
        /// <para>
        /// <c>divisor</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked /(Fraccion dividendo, Fraccion divisor)
        {
            return ((decimal)dividendo / (decimal)divisor).ToFraccion();
        }

        public static explicit operator checked byte(Fraccion fraccion)
        {
            return (byte)(decimal)fraccion;
        }

        public static explicit operator checked char(Fraccion fraccion)
        {
            return (char)(decimal)fraccion;
        }

        public static explicit operator checked short(Fraccion fraccion)
        {
            return (short)(decimal)fraccion;
        }

        public static explicit operator checked sbyte(Fraccion fraccion)
        {
            return (sbyte)(decimal)fraccion;
        }

        public static explicit operator checked UInt128(Fraccion fraccion)
        {
            return (UInt128)(decimal)fraccion;
        }

        public static explicit operator checked ushort(Fraccion fraccion)
        {
            return (ushort)(decimal)fraccion;
        }

        public static explicit operator checked uint(Fraccion fraccion)
        {
            return (uint)(decimal)fraccion;
        }

        public static explicit operator checked ulong(Fraccion fraccion)
        {
            return (ulong)(decimal)fraccion;
        }

        public static explicit operator checked nuint(Fraccion fraccion)
        {
            return (nuint)(decimal)fraccion;
        }

        public static explicit operator checked Fraccion(Int128 entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        public static explicit operator checked Fraccion(long entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        public static explicit operator checked Fraccion(nint entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        public static explicit operator checked Fraccion(UInt128 entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        public static explicit operator checked Fraccion(uint entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        public static explicit operator checked Fraccion(ulong entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        public static explicit operator checked Fraccion(nuint entero)
        {
            checked
            {
                return (int)entero;
            }
        }

        /// <summary>
        /// Incrementa un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// El resultado de incrementar
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked ++(Fraccion fraccion)
        {
            checked
            {
                return fraccion + UnEntero;
            }
        }

        /// <summary>
        /// Multiplica dos valores juntos para calcular su producto.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="factor1"></param>
        /// <returns>
        /// <para>
        /// El producto de
        /// </para>
        /// <para>
        /// <c>factor</c>
        /// </para>
        /// <para>
        /// multiplicado por
        /// </para>
        /// <para>
        /// <c>factor1</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked *(Fraccion factor, Fraccion factor1)
        {
            return ((decimal)factor * (decimal)factor1).ToFraccion();
        }

        /// <summary>
        /// Resta dos valores para calcular su diferencia.
        /// </summary>
        /// <param name="minuendo"></param>
        /// <param name="sustraendo"></param>
        /// <returns>
        /// <para>
        /// La diferencia de
        /// </para>
        /// <para>
        /// <c>sustraendo</c>
        /// </para>
        /// <para>
        /// restado de
        /// </para>
        /// <para>
        /// <c>minuendo</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked -(Fraccion minuendo, Fraccion sustraendo)
        {
            return ((decimal)minuendo - (decimal)sustraendo).ToFraccion();
        }

        /// <summary>
        /// Calcula la negación unaria de un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// La negación unaria de
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator checked -(Fraccion fraccion)
        {
            checked
            {
                return new(-fraccion.Numerador, fraccion.Denominador);
            }
        }

        /// <summary>
        /// Decrementa un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// El resultado de decrementar
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator --(Fraccion fraccion)
        {
            return fraccion - UnEntero;
        }

        /// <summary>
        /// Divide dos valores juntos para calcular su cociente.
        /// </summary>
        /// <param name="dividendo"></param>
        /// <param name="divisor"></param>
        /// <returns>
        /// <para>
        /// El cociente de
        /// </para>
        /// <para>
        /// <c>dividendo</c>
        /// </para>
        /// <para>
        /// dividido por
        /// </para>
        /// <para>
        /// <c>divisor</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator /(Fraccion dividendo, Fraccion divisor)
        {
            try
            {
                return ((decimal)dividendo / (decimal)divisor).ToFraccion();
            }
            catch (OverflowException)
            {
                return new(dividendo.Numerador * divisor.Denominador, dividendo.Denominador * divisor.Numerador);
            }
        }

        /// <summary>
        /// Compara dos valores para determinar igualdad.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <param name="fraccion1"></param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es igual a
        /// </para>
        /// <para>
        /// <c>fraccion1</c>
        /// </para>
        /// <para>
        /// ; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool operator ==(Fraccion fraccion, Fraccion fraccion1)
        {
            return (decimal)fraccion == (decimal)fraccion1;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="BigInteger"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(BigInteger entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="BigInteger"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una BigInteger.</returns>
        public static explicit operator BigInteger(Fraccion fraccion)
        {
            return (BigInteger)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="byte"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una byte.</returns>
        public static explicit operator byte(Fraccion fraccion)
        {
            return (byte)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="char"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una char.</returns>
        public static explicit operator char(Fraccion fraccion)
        {
            return (char)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="decimal"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a un decimal.</returns>
        public static explicit operator decimal(Fraccion fraccion)
        {
            return fraccion.Numerador / (decimal)fraccion.Denominador;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="Int128"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una Int128.</returns>
        public static explicit operator Int128(Fraccion fraccion)
        {
            return (Int128)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="short"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una short.</returns>
        public static explicit operator short(Fraccion fraccion)
        {
            return (short)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="int"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una int.</returns>
        public static explicit operator int(Fraccion fraccion)
        {
            return (int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="long"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una long.</returns>
        public static explicit operator long(Fraccion fraccion)
        {
            return (long)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="nint"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una nint.</returns>
        public static explicit operator nint(Fraccion fraccion)
        {
            return (nint)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="sbyte"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una sbyte.</returns>
        public static explicit operator sbyte(Fraccion fraccion)
        {
            return (sbyte)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="UInt128"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una UInt128.</returns>
        public static explicit operator UInt128(Fraccion fraccion)
        {
            return (UInt128)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="ushort"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una ushort.</returns>
        public static explicit operator ushort(Fraccion fraccion)
        {
            return (ushort)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="uint"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una uint.</returns>
        public static explicit operator uint(Fraccion fraccion)
        {
            return (uint)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="ulong"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una ulong.</returns>
        public static explicit operator ulong(Fraccion fraccion)
        {
            return (ulong)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte una fracción a un valor <see cref="nuint"/>.
        /// </summary>
        /// <param name="fraccion">El valor a convertir.</param>
        /// <returns><paramref name="fraccion"/> convertida a una nuint.</returns>
        public static explicit operator nuint(Fraccion fraccion)
        {
            return (nuint)(int)(decimal)fraccion;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="Int128"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(Int128 entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="long"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(long entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="nint"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(nint entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="UInt128"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(UInt128 entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="uint"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(uint entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="ulong"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(ulong entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Explícitamente convierte un valor <see cref="nuint"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static explicit operator Fraccion(nuint entero)
        {
            return (int)entero;
        }

        /// <summary>
        /// Compara dos valores para determinar cuál es mayor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <param name="fraccion1"></param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es mayor que
        /// </para>
        /// <para>
        /// <c>fraccion1</c>
        /// </para>
        /// <para>
        /// ; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool operator >(Fraccion fraccion, Fraccion fraccion1)
        {
            return (decimal)fraccion > (decimal)fraccion1;
        }

        /// <summary>
        /// Compara dos valores para determinar cuál es mayor o igual.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <param name="fraccion1"></param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es mayor o igual a
        /// </para>
        /// <para>
        /// <c>fraccion1</c>
        /// </para>
        /// <para>
        /// ; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool operator >=(Fraccion fraccion, Fraccion fraccion1)
        {
            return (fraccion > fraccion1) || (fraccion == fraccion1);
        }

        /// <summary>
        /// Implícitamente convierte un valor <see cref="byte"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static implicit operator Fraccion(byte entero)
        {
            return new(entero, 1);
        }

        /// <summary>
        /// Implícitamente convierte un valor <see cref="char"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static implicit operator Fraccion(char entero)
        {
            return new(entero, 1);
        }

        /// <summary>
        /// Implícitamente convierte un valor <see cref="short"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static implicit operator Fraccion(short entero)
        {
            return new(entero, 1);
        }

        /// <summary>
        /// Implícitamente convierte un valor <see cref="int"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static implicit operator Fraccion(int entero)
        {
            return new(entero, 1);
        }

        /// <summary>
        /// Implícitamente convierte un valor <see cref="sbyte"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static implicit operator Fraccion(sbyte entero)
        {
            return new(entero, 1);
        }

        /// <summary>
        /// Implícitamente convierte un valor <see cref="ushort"/> a una fracción.
        /// </summary>
        /// <param name="entero">El valor a convertir.</param>
        /// <returns><paramref name="entero"/> convertido a una fracción.</returns>
        public static implicit operator Fraccion(ushort entero)
        {
            return new(entero, 1);
        }

        /// <summary>
        /// Incrementa un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// El resultado de incrementar
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator ++(Fraccion fraccion)
        {
            return fraccion + UnEntero;
        }

        /// <summary>
        /// Compara dos valores para determinar desigualdad.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <param name="fraccion1"></param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// no es igual a
        /// </para>
        /// <para>
        /// <c>fraccion1</c>
        /// </para>
        /// <para>
        /// ; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool operator !=(Fraccion fraccion, Fraccion fraccion1)
        {
            return !(fraccion == fraccion1);
        }

        /// <summary>
        /// Compara dos valores para determinar cuál es menor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <param name="fraccion1"></param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es menor que
        /// </para>
        /// <para>
        /// <c>fraccion1</c>
        /// </para>
        /// <para>
        /// ; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool operator <(Fraccion fraccion, Fraccion fraccion1)
        {
            return (decimal)fraccion < (decimal)fraccion1;
        }

        /// <summary>
        /// Compara dos valores para determinar cuál es menor o igual.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <param name="fraccion1"></param>
        /// <returns>
        /// <para>
        /// <c>true</c>
        /// </para>
        /// <para>
        /// si
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// es menor o igual a
        /// </para>
        /// <para>
        /// <c>fraccion1</c>
        /// </para>
        /// <para>
        /// ; en caso contrario,
        /// </para>
        /// <para>
        /// <c>false</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static bool operator <=(Fraccion fraccion, Fraccion fraccion1)
        {
            return (fraccion < fraccion1) || (fraccion == fraccion1);
        }

        /// <summary>
        /// Divide dos valores juntos para calcular su módulo o residuo.
        /// </summary>
        /// <param name="dividendo"></param>
        /// <param name="divisor"></param>
        /// <returns>
        /// <para>
        /// El módulo o residuo de
        /// </para>
        /// <para>
        /// <c>dividendo</c>
        /// </para>
        /// <para>
        /// dividido por
        /// </para>
        /// <para>
        /// <c>divisor</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator %(Fraccion dividendo, Fraccion divisor)
        {
            return ((decimal)dividendo % (decimal)divisor).ToFraccion();
        }

        /// <summary>
        /// Multiplica dos valores juntos para calcular su producto.
        /// </summary>
        /// <param name="factor"></param>
        /// <param name="factor1"></param>
        /// <returns>
        /// <para>
        /// El producto de
        /// </para>
        /// <para>
        /// <c>factor</c>
        /// </para>
        /// <para>
        /// multiplicado por
        /// </para>
        /// <para>
        /// <c>factor1</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator *(Fraccion factor, Fraccion factor1)
        {
            try
            {
                return ((decimal)factor * (decimal)factor1).ToFraccion();
            }
            catch (OverflowException)
            {
                return new(factor.Numerador * factor1.Numerador, factor.Denominador * factor1.Denominador);
            }
        }

        /// <summary>
        /// Resta dos valores para calcular su diferencia.
        /// </summary>
        /// <param name="minuendo"></param>
        /// <param name="sustraendo"></param>
        /// <returns>
        /// <para>
        /// La diferencia de
        /// </para>
        /// <para>
        /// <c>sustraendo</c>
        /// </para>
        /// <para>
        /// restado de
        /// </para>
        /// <para>
        /// <c>minuendo</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator -(Fraccion minuendo, Fraccion sustraendo)
        {
            try
            {
                return ((decimal)minuendo - (decimal)sustraendo).ToFraccion();
            }
            catch (OverflowException)
            {
                return new(minuendo.Numerador * sustraendo.Denominador - minuendo.Denominador * sustraendo.Numerador, minuendo.Denominador * sustraendo.Denominador);
            }
        }

        /// <summary>
        /// Calcula la negación unaria de un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// La negación unaria de
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator -(Fraccion fraccion)
        {
            return new(-fraccion.Numerador, fraccion.Denominador);
        }

        /// <summary>
        /// Calcula el más unario de un valor.
        /// </summary>
        /// <param name="fraccion"></param>
        /// <returns>
        /// <para>
        /// El más unario de
        /// </para>
        /// <para>
        /// <c>fraccion</c>
        /// </para>
        /// <para>
        /// .
        /// </para>
        /// </returns>
        public static Fraccion operator +(Fraccion fraccion)
        {
            return new(+fraccion.Numerador, fraccion.Denominador);
        }
    }
}
