using System.Numerics;

namespace Forconsol
{
    /// <summary>
    /// Proporciona métodos de extensión para cálculos estadísticos.
    /// </summary>
    public static class Estadistica
    {
        /// <summary>
        /// Calcula la mediana de una secuencia de valores <see><typeparamref name="T"/></see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeros">Una secuencia de valores <typeparamref name="T"/> de la cual calcular la mediana.</param>
        /// <returns>La mediana de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static double Mediana<T>(this IEnumerable<T> numeros) where T : INumber<T>
        {   
            if (numeros == null)
            {
                throw new ArgumentNullException();
            }
            else if (numeros.Any())
            {
                int cantidadDeNumeros = numeros.Count();

                var numerosOrdenados = numeros.Order();

                return int.IsEvenInteger(cantidadDeNumeros) ? double.Parse((numerosOrdenados.ElementAt(cantidadDeNumeros / 2 - 1) + numerosOrdenados.ElementAt(cantidadDeNumeros / 2)).ToString()) / 2 : double.Parse(numerosOrdenados.ElementAt(cantidadDeNumeros / 2).ToString());
            }
            else
            {
                throw new InvalidOperationException();
            }
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
        /// Calcula el rango de una secuencia de valores <see><typeparamref name="T"/></see>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="numeros">Una secuencia de valores <typeparamref name="T"/> de la cual calcular el rango.</param>
        /// <returns>El rango de la secuencia de valores.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static double Rango<T>(this IEnumerable<T> numeros) where T : INumber<T>
        {
            if (numeros == null)
            {
                throw new ArgumentNullException();
            }
            else if (numeros.Any())
            {
                T valorMaximo = numeros.Max();
                T valorMinimo = numeros.Min();

                try
                {
                    return (double)((decimal)double.Parse(valorMaximo.ToString()) - (decimal)double.Parse(valorMinimo.ToString()));
                }
                catch (OverflowException)
                {
                    return double.Parse((valorMaximo - valorMinimo).ToString());
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
