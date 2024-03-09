namespace Forconsol
{
    /// <summary>
    /// Define un método generalizado que un value type o clase implementa para imprimir datos de una forma determinada.
    /// </summary>
    [Obsolete("Esta API será retirada en el futuro")]
    public interface IImprimible
    {
        /// <summary>
        /// Imprime los datos.
        /// </summary>
        void Imprimir();
    }
}
