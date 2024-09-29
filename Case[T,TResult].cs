namespace Forconsol
{
    /// <summary>
    /// Representa un par de patrón-resultado.
    /// </summary>
    /// <typeparam name="T">El tipo del objeto de prueba.</typeparam>
    /// <typeparam name="TResult">El tipo del resultado asociado a esta instancia.</typeparam>
    public sealed class Case<T, TResult>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Case{T, TResult}"/> con el predicado y el resultado especificados.
        /// </summary>
        /// <param name="pattern">La condición para probar a una <typeparamref name="T"/>.</param>
        /// <param name="result">El resultado de esta instancia.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Case(Predicate<T?> pattern, TResult? result)
        {
            ArgumentNullException.ThrowIfNull(pattern);

            Pattern = pattern;
            this.result = () => result;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Case{T, TResult}"/> con el predicado y la función suministradora especificados.
        /// </summary>
        /// <param name="pattern">La condición para probar a una <typeparamref name="T"/>.</param>
        /// <param name="result">La función que devuelve el resultado de esta instancia.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Case(Predicate<T?> pattern, Func<TResult?> result)
        {
            if (pattern is null || result is null)
                throw new ArgumentNullException();

            Pattern = pattern;
            this.result = result;
        }

        private readonly Func<TResult?> result;

        /// <summary>
        /// Suministra el resultado asociado con esta instancia.
        /// </summary>
        /// <returns>Una <typeparamref name="TResult"/>.</returns>
        public TResult? EvaluatedResult { get => result(); }

        /// <summary>
        /// Obtiene el patrón asociado con esta instancia.
        /// </summary>
        /// <returns>Una <see cref="Predicate{T}"/>.</returns>
        public Predicate<T?> Pattern { get; }
    }
}