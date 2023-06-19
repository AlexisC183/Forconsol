namespace Forconsol
{
    /// <summary>
    /// Representa una tabla de transporte que se puede imprimir en consola.
    /// </summary>
    /// <typeparam name="T">El tipo de los elementos de la tabla.</typeparam>
    public class TablaTransporteImprimible<T> : IImprimible
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TablaTransporteImprimible{T}"/> con una tabla especificada.
        /// </summary>
        /// <param name="tabla">Un arreglo bidimensional.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TablaTransporteImprimible(T[,] tabla)
        {
            if (tabla == null)
            {
                throw new ArgumentNullException();
            }
            this.tabla = tabla;
            longitudesDatosMayores = new byte[tabla.GetLength(1)];
        }

        private byte i, j;
        private byte[] longitudesDatosMayores;
        private T[,] tabla;

        private void DeterminarLongitudesDatosMayores()
        {
            for (i = 0; i < tabla.GetLength(1); i++)
            {
                for (j = 0; j < tabla.GetLength(0); j++)
                {
                    if (tabla[j, i] != null)
                    {
                        if (longitudesDatosMayores[i] < tabla[j, i].ToString().Length)
                        {
                            longitudesDatosMayores[i] = (byte)tabla[j, i].ToString().Length;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Imprime la tabla de transporte en consola.
        /// </summary>
        public void Imprimir()
        {
            DeterminarLongitudesDatosMayores();

            for (i = 0; i < tabla.GetLength(0); i++)
            {
                for (j = 0; j < tabla.GetLength(1); j++)
                {
                    Console.Write(((tabla[i, j] == null) ? "".PadLeft(longitudesDatosMayores[j]) : tabla[i, j].ToString().PadLeft(longitudesDatosMayores[j])) + ' ');

                    if (byte.IsEvenInteger(j))
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
