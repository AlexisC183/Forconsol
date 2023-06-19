namespace Forconsol
{
    /// <summary>
    /// Representa un generador de números primos.
    /// </summary>
    public class NumeroPrimo
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="NumeroPrimo"/> lista para calcular números primos.
        /// </summary>
        public NumeroPrimo() 
        {
            numeroIncrementable = 1;
        }

        private int numeroIncrementable;

        /// <summary>
        /// Devuelve el siguiente número primo.
        /// </summary>
        /// <returns>Un int primo.</returns>
        public int Next()
        {
            byte divisionesExactas;
            int divisor;

            do
            {
                divisionesExactas = 2;
                divisor = 2;
                numeroIncrementable++;

                while (divisionesExactas < 3 && divisor < numeroIncrementable)
                {
                    if (numeroIncrementable % divisor == 0)
                    {
                        divisionesExactas++;
                    }
                    divisor++;
                }
            }
            while (divisionesExactas != 2);

            return numeroIncrementable;
        }

        /// <summary>
        /// Reinicia esta instancia a su estado inicial.
        /// </summary>
        public void Reset()
        {
            numeroIncrementable = 1;
        }
    }
}
