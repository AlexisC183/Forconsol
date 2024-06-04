using System.Numerics;

namespace Forconsol
{
    /// <summary>
    /// Proporciona métodos de extensión para cálculos estadísticos.
    /// </summary>
    public static class Estadistica
    {
        /// <summary>
        /// Calcula la mediana de una secuencia de valores <see langword="decimal"/>.
        /// </summary>
        /// <param name="numeros">Una secuencia de valores <see langword="decimal"/> de la cual calcular la mediana.</param>
        /// <returns>La mediana de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Mediana(this IEnumerable<decimal> numeros)
        {
            if (numeros == null) 
            {
                throw new ArgumentNullException();
            }
            else if (numeros.Any())
            {
                int cantidadDeNumeros = numeros.Count();

                var numerosOrdenados = numeros.Order();

                return
                int.IsEvenInteger(cantidadDeNumeros)
                ?
                    numerosOrdenados.ElementAt(cantidadDeNumeros / 2 - 1) / 2 + numerosOrdenados.ElementAt(cantidadDeNumeros / 2) / 2
                :
                    numerosOrdenados.ElementAt(cantidadDeNumeros / 2);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Calcula la mediana de una secuencia de valores <see langword="int"/>.
        /// </summary>
        /// <param name="numeros">Una secuencia de valores <see langword="int"/> de la cual calcular la mediana.</param>
        /// <returns>La mediana de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Mediana(this IEnumerable<int> numeros)
        {
            IEnumerable<decimal> aDecimal = numeros.Select(i => (decimal)i);

            return aDecimal.Mediana();
        }

        /// <summary>
        /// Calcula la mediana de una secuencia de valores <see langword="long"/>.
        /// </summary>
        /// <param name="numeros">Una secuencia de valores <see langword="long"/> de la cual calcular la mediana.</param>
        /// <returns>La mediana de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Mediana(this IEnumerable<long> numeros)
        {
            IEnumerable<decimal> aDecimal = numeros.Select(l => (decimal)l);

            return aDecimal.Mediana();
        }

        /// <summary>
        /// Calcula la mediana de una secuencia de valores <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeros">Una secuencia de valores <typeparamref name="T"/> de la cual calcular la mediana.</param>
        /// <returns>La mediana de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Mediana<T>(this IEnumerable<T> numeros) where T : IConvertibleADecimal<T>
        {
            IEnumerable<decimal> aDecimal = numeros.Select(n => (decimal)n);

            return aDecimal.Mediana();
        }

        /// <summary>
        /// Calcula la moda de una secuencia de valores <see><typeparamref name="T"/></see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeros">Una secuencia de valores <typeparamref name="T"/> de la cual calcular la moda.</param>
        /// <returns>Una <see cref="IEnumerable{T}"/> que contiene la moda de la secuencia fuente.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> Moda<T>(this IEnumerable<T> numeros) where T : INumber<T>
        {
            if (numeros == null)
            {
                throw new ArgumentNullException();
            }
            else if (numeros.Any())
            {
                int cantidadNoDuplicado;

                List<T> noDuplicados = numeros.Distinct().ToList();
                List<int> cantidadesNoDuplicados = new(noDuplicados.Count);

                foreach (T noDuplicado in noDuplicados)
                {
                    cantidadNoDuplicado = 0;
                    foreach (T numero in numeros)
                    {
                        if (numero == noDuplicado)
                        {
                            cantidadNoDuplicado++;
                        }
                    }

                    cantidadesNoDuplicados.Add(cantidadNoDuplicado);
                }

                int cantidadMayor = cantidadesNoDuplicados.Max();

                return from noDuplicado in noDuplicados
                       where cantidadesNoDuplicados[noDuplicados.IndexOf(noDuplicado)] == cantidadMayor
                       select noDuplicado;
            }
            else
            {
                return Enumerable.Empty<T>();
            }
        }

        /// <summary>
        /// Calcula la moda de una secuencia de valores <see><typeparamref name="T"/></see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeros">Una secuencia de valores <typeparamref name="T"/> de la cual calcular la moda.</param>
        /// <returns>Una <see cref="IEnumerable{T}"/> que contiene la moda de la secuencia fuente.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<decimal> ModaDecimales<T>(this IEnumerable<T> numeros) where T : IConvertibleADecimal<T>
        {
            IEnumerable<decimal> aDecimal = numeros.Select(n => (decimal)n);

            return aDecimal.Moda();
        }

        /// <summary>
        /// Calcula el rango de una secuencia de valores <see langword="decimal"/>.
        /// </summary>
        /// <param name="numeros">Una secuencia de valores <see langword="decimal"/> de la cual calcular el rango.</param>
        /// <returns>El rango de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static decimal Rango(this IEnumerable<decimal> numeros)
        {
            if (numeros == null)
            {
                throw new ArgumentNullException();
            }
            else if (numeros.Any())
            {
                return numeros.Max() - numeros.Min();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// Calcula el rango de una secuencia de valores <see langword="int"/>.
        /// </summary>
        /// <param name="numeros">Una secuencia de valores <see langword="int"/> de la cual calcular el rango.</param>
        /// <returns>El rango de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Rango(this IEnumerable<int> numeros)
        {
            IEnumerable<decimal> aDecimal = numeros.Select(i => (decimal)i);

            return aDecimal.Rango();
        }

        /// <summary>
        /// Calcula el rango de una secuencia de valores <see langword="long"/>.
        /// </summary>
        /// <param name="numeros">Una secuencia de valores <see langword="long"/> de la cual calcular el rango.</param>
        /// <returns>El rango de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Rango(this IEnumerable<long> numeros)
        {
            IEnumerable<decimal> aDecimal = numeros.Select(l => (decimal)l);

            return aDecimal.Rango();
        }

        /// <summary>
        /// Calcula el rango de una secuencia de valores <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeros">Una secuencia de valores <typeparamref name="T"/> de la cual calcular el rango.</param>
        /// <returns>El rango de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static decimal Rango<T>(this IEnumerable<T> numeros) where T : IConvertibleADecimal<T>
        {
            IEnumerable<decimal> aDecimal = numeros.Select(n => (decimal)n);

            return aDecimal.Rango();
        }
    }
}