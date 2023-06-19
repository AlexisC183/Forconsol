namespace Forconsol
{
    /// <summary>
    /// Representa una solicitud de entrada de un valor por el usuario y proporciona un mecanismo para que dicho valor siempre sea el correcto.
    /// </summary>
    /// <typeparam name="TResultado">El tipo de la entrada a solicitar.</typeparam>
    public class SolicitudDeEntrada<TResultado>
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SolicitudDeEntrada{TResultado}"/> con un método Parse o similar especificado.
        /// </summary>
        /// <param name="metodo">El delegado que representa el código que convierte una cadena a <typeparamref name="TResultado"/>.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SolicitudDeEntrada(MetodoParse<TResultado> metodo)
        {
            if (metodo == null)
            {
                throw new ArgumentNullException();
            }
            metodoParse = metodo;
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="SolicitudDeEntrada{TResultado}"/> con un método TryParse o similar especificado.
        /// </summary>
        /// <param name="metodo">El delegado que representa el código que convierte una cadena, asigna una variable <typeparamref name="TResultado"/> y devuelve un bool.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SolicitudDeEntrada(MetodoTryParse<TResultado> metodo)
        {
            if (metodo == null)
            {
                throw new ArgumentNullException();
            }
            metodoTryParse = metodo;
        }

        private MetodoParse<TResultado> metodoParse;
        private MetodoTryParse<TResultado> metodoTryParse;

        /// <summary>
        /// Obtiene o establece la condición, o null si ninguna fue suministrada.
        /// </summary>
        /// <returns>Una <see cref="Predicate{TResultado}"/> que se aplica al validar la entrada del usuario.</returns>
        public Predicate<TResultado>? Condicion { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de error de la solicitud.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime cuando la entrada del usuario no es válida.</returns>
        public string MensajeDeError { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de solicitud de entrada.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime cuando se solicita al usuario una entrada.</returns>
        public string MensajeDeSolicitud { get; set; }

        /// <summary>
        /// Solicita la entrada.
        /// </summary>
        /// <param name="variable">Una variable donde se asigna la entrada.</param>
        public void Solicitar(out TResultado variable)
        {
            if (metodoParse != null)
            {
                SolicitarConMetodoParse(out variable);
            }
            else
            {
                SolicitarConMetodoTryParse(out variable);
            }
        }

        private void SolicitarConMetodoParse(out TResultado variable)
        {
            while (true)
            {
                Console.Write(MensajeDeSolicitud ?? "Introducir un " + typeof(TResultado) + ": ");
                try
                {
                    variable = metodoParse(Console.ReadLine());
                    if (Condicion != null && !Condicion(variable))
                    {
                        throw new ArgumentException();
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine(MensajeDeError ?? "¡Entrada no válida!");
                }
            }
        }

        private void SolicitarConMetodoTryParse(out TResultado variable)
        {
            while (true)
            {
                Console.Write(MensajeDeSolicitud ?? "Introducir un " + typeof(TResultado) + ": ");
                if (metodoTryParse(Console.ReadLine(), out variable) && (Condicion == null || Condicion(variable)))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(MensajeDeError ?? "¡Entrada no válida!");
                }
            }
        } 
    }

    /// <summary>
    /// Encapsula un método que acepta como argumento la representación en cadena de una instancia y que devuelve dicha cadena convertida a <typeparamref name="TResultado"/>.
    /// </summary>
    /// <typeparam name="TResultado">El tipo del valor devuelto del método que este delegado encapsula.</typeparam>
    /// <param name="cadenaTResultado">Una cadena que contiene una instancia a convertir.</param>
    /// <returns>El valor devuelto del método que este delegado encapsula.</returns>
    public delegate TResultado MetodoParse<TResultado>(string cadenaTResultado);

    /// <summary>
    /// Encapsula un método que acepta como argumentos la representación en cadena de una instancia y una variable de salida con dicha instancia; el método devuelve un bool.
    /// </summary>
    /// <typeparam name="TResultado">El tipo de la variable de salida del método que este delegado encapsula.</typeparam>
    /// <param name="cadenaTResultado">Una cadena que contiene una instancia a convertir.</param>
    /// <param name="variable">Una variable donde se asigna la instancia.</param>
    /// <returns><see langword="true"/> si <paramref name="cadenaTResultado"/> se convirtió con éxito; en caso contrario, <see langword="false"/>.</returns>
    public delegate bool MetodoTryParse<TResultado>(string cadenaTResultado, out TResultado variable);
}
