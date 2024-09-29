namespace Forconsol
{
    /// <summary>
    /// Representa una colección de casos encapsulados, los cuales son pares de patrón-resultado.
    /// </summary>
    /// <typeparam name="T">El tipo del objeto que se hará coincidir con los casos de esta instancia.</typeparam>
    /// <typeparam name="TResult">El tipo de resultado devuelto por esta instancia.</typeparam>
    public class CaseCollection<T, TResult>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CaseCollection{T, TResult}"/> con las instancias <see cref="Case{T, TResult}"/> especificadas.
        /// </summary>
        /// <param name="cases">Los casos que componen esta colección.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CaseCollection(params Case<T?, TResult?>[] cases)
        {
            ArgumentNullException.ThrowIfNull(cases);

            this.cases = cases.Cast<Case<T?, TResult?>>();
        }

        private readonly IEnumerable<Case<T?, TResult?>> cases;

        /// <summary>
        /// Devuelve el resultado del primer caso en el que la <typeparamref name="T"/> proporcionada coincide, o el valor predeterminado de <typeparamref name="TResult"/> si no se encuentran coincidencias.
        /// </summary>
        /// <param name="object_">El objeto de prueba.</param>
        /// <returns>La <typeparamref name="TResult"/> resultante de la coincidencia de patrones.</returns>
        public TResult? FirstResultOrDefault(T? object_)
        {
            Case<T?, TResult?>? match = cases.FirstOrDefault(c => c is not null && c.Pattern(object_));

            return match is null ? default : match.EvaluatedResult;
        }

        /// <summary>
        /// Devuelve el resultado del primer caso en el que la <typeparamref name="T"/> proporcionada coincide, o la <typeparamref name="TResult"/> proporcionada si no se encuentran coincidencias.
        /// </summary>
        /// <param name="object_">El objeto de prueba.</param>
        /// <param name="default_">El objeto a devolver en caso de que no se encuentren coincidencias.</param>
        /// <returns>La <typeparamref name="TResult"/> resultante de la coincidencia de patrones.</returns>
        public TResult? FirstResultOrDefault(T? object_, TResult? default_)
        {
            Case<T?, TResult?>? match = cases.FirstOrDefault(c => c is not null && c.Pattern(object_));

            return match is null ? default_ : match.EvaluatedResult;
        }

        /// <summary>
        /// Devuelve el resultado del primer caso en el que la <typeparamref name="T"/> proporcionada coincide, o la <typeparamref name="TResult"/> suministrada por la función proporcionada si no se encuentran coincidencias.
        /// </summary>
        /// <param name="object_">El objeto de prueba.</param>
        /// <param name="default_">La función que suministra el resultado cuando no se encuentran coincidencias.</param>
        /// <returns>La <typeparamref name="TResult"/> resultante de la coincidencia de patrones.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public TResult? FirstResultOrDefault(T? object_, Func<TResult?> default_)
        {
            ArgumentNullException.ThrowIfNull(default_);

            Case<T?, TResult?>? match = cases.FirstOrDefault(c => c is not null && c.Pattern(object_));

            return match is null ? default_() : match.EvaluatedResult;
        }
    }
}