namespace Forconsol
{
    /// <summary>
    /// Define un tipo que se puede convertir a <see cref="decimal"/>.
    /// </summary>
    /// <typeparam name="TMismo">El tipo que implementa la interfaz.</typeparam>
    public interface IConvertibleADecimal<TMismo> where TMismo : IConvertibleADecimal<TMismo>
    {
        /// <summary>
        /// Explícitamente convierte un valor <typeparamref name="TMismo"/> a un decimal.
        /// </summary>
        /// <param name="instancia">El valor a convertir.</param>
        /// <returns><paramref name="instancia"/> convertida a un decimal.</returns>
        /// <exception cref="OverflowException"></exception>
        static virtual explicit operator checked decimal(TMismo instancia)
        {
            return (decimal)instancia;
        }

        /// <summary>
        /// Explícitamente convierte un valor <typeparamref name="TMismo"/> a un decimal.
        /// </summary>
        /// <param name="instancia">El valor a convertir.</param>
        /// <returns><paramref name="instancia"/> convertida a un decimal.</returns>
        static abstract explicit operator decimal(TMismo instancia);
    }
}