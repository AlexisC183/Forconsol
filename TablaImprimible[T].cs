namespace Forconsol
{
    /// <summary>
    /// Representa una tabla de objetos que se puede imprimir en consola.
    /// </summary>
    /// <typeparam name="T">El tipo de los elementos de la tabla.</typeparam>
    public class TablaImprimible<T> : IImprimible
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="TablaImprimible{T}"/> con una tabla especificada.
        /// </summary>
        /// <param name="tabla">Un arreglo bidimensional.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TablaImprimible(T[,] tabla)
        {
            if (tabla == null)
            {
                throw new ArgumentNullException();
            }
            this.tabla = tabla;

            if (tabla.GetType().GetElementType().IsNumber())
            {
                AlineacionDeElementos = AlineacionDeElementos.Derecha;
            }

            longitudesDatosMayores = new byte[tabla.GetLength(1)];
        }

        private byte i, j;
        private byte[] longitudesDatosMayores;
        private T[,] tabla;

        /// <summary>
        /// Obtiene o establece la alineación de los elementos convertidos a cadena.
        /// </summary>
        /// <returns>Una <see cref="AlineacionDeElementos"/> de la instancia actual. La predeterminada es derecha si el tipo predefinido de los elementos de la tabla es un número.</returns>
        public AlineacionDeElementos AlineacionDeElementos { get; set; }

        /// <summary>
        /// Obtiene o establece un valor indicando si la tabla muestra separadores de celdas.
        /// </summary>
        /// <returns><see langword="true"/> si la tabla muestra separadores de celdas; en caso contrario, <see langword="false"/>. El predeterminado es false.</returns>
        public bool Separadores { get; set; }

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
        /// Imprime la tabla en consola.
        /// </summary>
        public void Imprimir()
        {
            DeterminarLongitudesDatosMayores();

            for (i = 0; i < tabla.GetLength(0); i++)
            {
                for (j = 0; j < tabla.GetLength(1); j++)
                {
                    Console.Write
                    (
                        (
                            (AlineacionDeElementos == AlineacionDeElementos.Izquierda)
                            ?
                                (tabla[i, j] == null) ? "".PadRight(longitudesDatosMayores[j]) : tabla[i, j].ToString().PadRight(longitudesDatosMayores[j])
                            :
                                (tabla[i, j] == null) ? "".PadLeft(longitudesDatosMayores[j]) : tabla[i, j].ToString().PadLeft(longitudesDatosMayores[j])
                        ) + ' '
                    );

                    if (Separadores)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();
            }
        }      
    }

    /// <summary>
    /// Especifica constantes que definen una alineación de elementos.
    /// </summary>
    public enum AlineacionDeElementos
    {
        /// <summary>
        /// Alineación a la izquierda.
        /// </summary>
        Izquierda,
        
        /// <summary>
        /// Alineación a la derecha.
        /// </summary>
        Derecha
    }
}
