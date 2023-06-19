namespace Forconsol
{
    /// <summary>
    /// Representa un menú que solicita al usuario una opción a elegir y luego ejecuta dicha opción.
    /// </summary>
    public class MenuDeOpciones
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MenuDeOpciones"/> con un diccionario de opciones especificado.
        /// </summary>
        /// <param name="opciones">Un diccionario que almacena los rótulos y los procedimientos de las opciones a elegir.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MenuDeOpciones(Dictionary<string, Action> opciones)
        {
            if (opciones == null)
            {
                throw new ArgumentNullException();
            }
            this.opciones = opciones;
            SetEstadoInicial();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MenuDeOpciones"/> con un diccionario de opciones especificado y si el menú contiene una opción de salir.
        /// </summary>
        /// <param name="opciones">Un diccionario que almacena los rótulos y los procedimientos de las opciones a elegir.</param>
        /// <param name="anadirOpcionDeSalir">true para añadir una opción de salir; en caso contrario, false.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MenuDeOpciones(Dictionary<string, Action> opciones, bool anadirOpcionDeSalir)
        {
            if (opciones == null)
            {
                throw new ArgumentNullException();
            }
            this.opciones = opciones;
            hasOpcionDeSalir = anadirOpcionDeSalir;
            SetEstadoInicial();
        }

        private char caracterEstiloDeNumeracion, letraDeOpcion;
        private bool hasOpcionDeSalir;
        private byte i, longitudDeEstiloDeNumeracionNumero, numeroDeOpcion;
        private static int numeroDeInstancia = 0;
        private Dictionary<string, Action> opciones;
        private SolicitudDeEntrada<char> solicitudDeLetra;
        private SolicitudDeEntrada<byte> solicitudDeNumero;

        /// <summary>
        /// Obtiene o establece la acción antes de desplegar el menú, o null si ninguna fue suministrada.
        /// </summary>
        /// <returns>Una <see cref="Action"/> que se ejecuta antes de desplegar el menú.</returns>
        public Action? AccionAntesDeDesplegarMenu { get; set; }

        /// <summary>
        /// Obtiene o establece la acción antes de salir del menú, o null si ninguna fue suministrada.
        /// </summary>
        /// <returns>Una <see cref="Action"/> que se ejecuta antes de salir del menú.</returns>
        public Action? AccionAntesDeSalir { get; set; }

        /// <summary>
        /// Obtiene o establece un valor indicando si se borra la pantalla antes de iniciar el menú.
        /// </summary>
        /// <returns><see langword="true"/> si se borra la pantalla antes de iniciar el menú; en caso contrario, <see langword="false"/>. El predeterminado es true.</returns>
        public bool BorrarPantallaAntesDeIniciar { get; set; }

        /// <summary>
        /// Obtiene o establece un valor indicando si se borra la pantalla antes de iniciar una opción.
        /// </summary>
        /// <returns><see langword="true"/> si se borra la pantalla antes de iniciar una opción; en caso contrario, <see langword="false"/>. El predeterminado es false.</returns>
        public bool BorrarPantallaAntesDeIniciarOpcion { get; set; }

        /// <summary>
        /// Obtiene o establece el estilo de numeración del menú.
        /// </summary>
        /// <returns>Una <see cref="EstiloDeNumeracion"/> de la instancia actual. La predeterminada es minúscula-paréntesis.</returns>
        public EstiloDeNumeracion EstiloDeNumeracion { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de error del menú.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime cuando el usuario introduce una opción errónea.</returns>
        public string MensajeDeError { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de fin de opción del menú, o null si ninguno fue suministrado.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime cuando se finaliza una opción del menú. La predeterminada es "Presionar cualquier tecla para aceptar.".</returns>
        public string? MensajeDeFinDeOpcion { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de solicitud de letra del menú.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime cuando se solicita al usuario una letra de opción a elegir.</returns>
        public string MensajeDeSolicitudDeLetra { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje de solicitud de número del menú.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime cuando se solicita al usuario un número de opción a elegir.</returns>
        public string MensajeDeSolicitudDeNumero { get; set; }

        /// <summary>
        /// Obtiene o establece el rótulo de la opción de salir del menú.
        /// </summary>
        /// <returns>Una <see cref="string"/> que representa el rótulo de la opción de salir del menú.</returns>
        public string RotuloOpcionDeSalir { get; set; }

        /// <summary>
        /// Obtiene o establece el título del menú, o null si ninguno fue suministrado.
        /// </summary>
        /// <returns>Una <see cref="string"/> que se imprime como cabecera del menú. La predeterminada es "Menú de opciones" más un posible número de instancia.</returns>
        public string? Titulo { get; set; }

        private void AplicarAccionAntesDeSalir()
        {
            if (AccionAntesDeSalir != null)
            {
                AccionAntesDeSalir();
            }
        }

        private void AplicarBorrarPantallaAntesDeIniciarOpcion()
        {
            if (BorrarPantallaAntesDeIniciarOpcion)
            {
                Console.Clear();
            }
        }

        private void AplicarMensajeFinDeOpcion()
        {
            if (MensajeDeFinDeOpcion != null)
            {
                Console.WriteLine(MensajeDeFinDeOpcion);
                Console.ReadKey();
            }
        }

        private void AplicarPropiedades()
        {
            if (BorrarPantallaAntesDeIniciar)
            {
                Console.Clear();
            }
            if (AccionAntesDeDesplegarMenu != null)
            {
                AccionAntesDeDesplegarMenu();
            }
            if (Titulo != null)
            {
                Console.WriteLine(Titulo);
            }
        }

        private void ImprimirOpcionesConEstiloMayuscula()
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                Console.WriteLine((char)i++ + (caracterEstiloDeNumeracion + " ") + opcion.Key);
                if (i == 91)
                {
                    break;
                }
            }
        }

        private void ImprimirOpcionesConEstiloMinuscula()
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                Console.WriteLine((char)i++ + (caracterEstiloDeNumeracion + " ") + opcion.Key);
                if (i == 123)
                {
                    break;
                }
            }
        }

        private void ImprimirOpcionesConEstiloNumero()
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                Console.WriteLine((i++ + caracterEstiloDeNumeracion.ToString()).PadRight(longitudDeEstiloDeNumeracionNumero) + opcion.Key);
                if (i == 255)
                {
                    break;
                }
            }
        }

        private void ImprimirOpcionesConOpcionDeSalirYConEstiloMayuscula()
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                Console.WriteLine((char)i + (caracterEstiloDeNumeracion + " ") + opcion.Key);
                if (++i == 90)
                {
                    break;
                }
            }
            Console.WriteLine((char)i + (caracterEstiloDeNumeracion + " ") + (RotuloOpcionDeSalir ?? "Salir"));
        }

        private void ImprimirOpcionesConOpcionDeSalirYConEstiloMinuscula()
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                Console.WriteLine((char)i + (caracterEstiloDeNumeracion + " ") + opcion.Key);
                if (++i == 122)
                {
                    break;
                }
            }
            Console.WriteLine((char)i + (caracterEstiloDeNumeracion + " ") + (RotuloOpcionDeSalir ?? "Salir"));
        }

        private void ImprimirOpcionesConOpcionDeSalirYConEstiloNumero()
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                Console.WriteLine((i + caracterEstiloDeNumeracion.ToString()).PadRight(longitudDeEstiloDeNumeracionNumero) + opcion.Key);
                if (++i == 255)
                {
                    break;
                }
            }
            Console.WriteLine((i + caracterEstiloDeNumeracion.ToString()).PadRight(longitudDeEstiloDeNumeracionNumero) + (RotuloOpcionDeSalir ?? "Salir"));
        }

        private void InicializarMenuConEstiloLetra()
        {
            solicitudDeLetra = new(char.TryParse)
            {
                MensajeDeSolicitud = MensajeDeSolicitudDeLetra ?? "Introducir letra de opción deseada: ",
                MensajeDeError = MensajeDeError ?? "¡Opción no válida!"
            };

            if (EstiloDeNumeracion == EstiloDeNumeracion.MinusculaParentesis || EstiloDeNumeracion == EstiloDeNumeracion.MinusculaPunto)
            {
                i = 97;

                ImprimirOpcionesConEstiloMinuscula();
                i -= 32;
            }
            else
            {
                i = 65;

                ImprimirOpcionesConEstiloMayuscula();
            }
            solicitudDeLetra.Condicion = (letraDeOpcion) => (letraDeOpcion >= 97 && letraDeOpcion < i + 32) || (letraDeOpcion >= 65 && letraDeOpcion < i);
        }

        private void InicializarMenuConEstiloNumero()
        {
            solicitudDeNumero = new(byte.TryParse)
            {
                MensajeDeSolicitud = MensajeDeSolicitudDeNumero ?? "Introducir número de opción deseada: ",
                MensajeDeError = MensajeDeError ?? "¡Opción no válida!"
            };

            i = 1;

            SetLongitudDeEstiloDeNumeracionNumero();
            ImprimirOpcionesConEstiloNumero();
            solicitudDeNumero.Condicion = (numeroDeOpcion) => numeroDeOpcion >= 1 && numeroDeOpcion < i;
        }

        private void InicializarMenuConOpcionDeSalirYConEstiloLetra()
        {
            solicitudDeLetra = new(char.TryParse)
            {
                MensajeDeSolicitud = MensajeDeSolicitudDeLetra ?? "Introducir letra de opción deseada: ",
                MensajeDeError = MensajeDeError ?? "¡Opción no válida!"
            };

            if (EstiloDeNumeracion == EstiloDeNumeracion.MinusculaParentesis || EstiloDeNumeracion == EstiloDeNumeracion.MinusculaPunto)
            {
                i = 97;

                ImprimirOpcionesConOpcionDeSalirYConEstiloMinuscula();
                i -= 32;
            }
            else
            {
                i = 65;

                ImprimirOpcionesConOpcionDeSalirYConEstiloMayuscula();
            }
            solicitudDeLetra.Condicion = (letraDeOpcion) => (letraDeOpcion >= 97 && letraDeOpcion <= i + 32) || (letraDeOpcion >= 65 && letraDeOpcion <= i);
        }

        private void InicializarMenuConOpcionDeSalirYConEstiloNumero()
        {
            solicitudDeNumero = new(byte.TryParse)
            {
                MensajeDeSolicitud = MensajeDeSolicitudDeNumero ?? "Introducir número de opción deseada: ",
                MensajeDeError = MensajeDeError ?? "¡Opción no válida!"
            };

            i = 1;

            SetLongitudDeEstiloDeNumeracionNumero();
            ImprimirOpcionesConOpcionDeSalirYConEstiloNumero();
            solicitudDeNumero.Condicion = (numeroDeOpcion) => numeroDeOpcion >= 1 && numeroDeOpcion <= i;
        }

        /// <summary>
        /// Inicia el menú de opciones.
        /// </summary>
        public void Iniciar()
        {
            if (hasOpcionDeSalir)
            {
                IniciarMenuConOpcionDeSalir();
            }
            else
            {
                IniciarMenu();
            }
        }

        private void IniciarMenu()
        {
            while (true)
            {
                AplicarPropiedades();
                SetCaracterEstiloDeNumeracion();
                if (EstiloDeNumeracion == EstiloDeNumeracion.NumeroParentesis || EstiloDeNumeracion == EstiloDeNumeracion.NumeroPunto)
                {
                    InicializarMenuConEstiloNumero();
                    solicitudDeNumero.Solicitar(out numeroDeOpcion);
                    AplicarBorrarPantallaAntesDeIniciarOpcion();
                    i = 1;

                    IniciarOpcionDeseada(numeroDeOpcion);
                }
                else
                {
                    InicializarMenuConEstiloLetra();
                    solicitudDeLetra.Solicitar(out letraDeOpcion);
                    char.TryParse(letraDeOpcion.ToString().ToUpper(), out letraDeOpcion);
                    AplicarBorrarPantallaAntesDeIniciarOpcion();
                    i = 65;

                    IniciarOpcionDeseada((byte)letraDeOpcion);
                }
                AplicarMensajeFinDeOpcion();
            }
        }

        private void IniciarMenuConOpcionDeSalir()
        {
            while (true)
            {
                AplicarPropiedades();
                SetCaracterEstiloDeNumeracion();
                if (EstiloDeNumeracion == EstiloDeNumeracion.NumeroParentesis || EstiloDeNumeracion == EstiloDeNumeracion.NumeroPunto)
                {
                    InicializarMenuConOpcionDeSalirYConEstiloNumero();
                    solicitudDeNumero.Solicitar(out numeroDeOpcion);
                    AplicarBorrarPantallaAntesDeIniciarOpcion();
                    if (numeroDeOpcion == i)
                    {
                        AplicarAccionAntesDeSalir();
                        AplicarMensajeFinDeOpcion();
                        break;
                    }
                    i = 1;

                    IniciarOpcionDeseada(numeroDeOpcion);
                }
                else
                {
                    InicializarMenuConOpcionDeSalirYConEstiloLetra();
                    solicitudDeLetra.Solicitar(out letraDeOpcion);
                    char.TryParse(letraDeOpcion.ToString().ToUpper(), out letraDeOpcion);
                    AplicarBorrarPantallaAntesDeIniciarOpcion();
                    if (letraDeOpcion == i)
                    {
                        AplicarAccionAntesDeSalir();
                        AplicarMensajeFinDeOpcion();
                        break;
                    }
                    i = 65;

                    IniciarOpcionDeseada((byte)letraDeOpcion);
                }
                AplicarMensajeFinDeOpcion();
            }
        }

        private void IniciarOpcionDeseada(byte opcionDeseada)
        {
            foreach (KeyValuePair<string, Action> opcion in opciones)
            {
                if (opcionDeseada == i++)
                {
                    opcion.Value();
                    break;
                }
            }
        }

        private void SetCaracterEstiloDeNumeracion()
        {
            caracterEstiloDeNumeracion = (EstiloDeNumeracion == EstiloDeNumeracion.MinusculaParentesis || EstiloDeNumeracion == EstiloDeNumeracion.MayusculaParentesis || EstiloDeNumeracion == EstiloDeNumeracion.NumeroParentesis) ? ')' : '.';
        }

        private void SetEstadoInicial()
        {
            BorrarPantallaAntesDeIniciar = true;
            Titulo = "Menú de opciones" + ((numeroDeInstancia == 0) ? null : " " + numeroDeInstancia);
            MensajeDeFinDeOpcion = "Presionar cualquier tecla para aceptar.";
            numeroDeInstancia++;
        }

        private void SetLongitudDeEstiloDeNumeracionNumero()
        {
            longitudDeEstiloDeNumeracionNumero =
            (byte)
            (
                hasOpcionDeSalir
                ?
                    (opciones.Count < 9)
                    ?
                        3
                    :
                        (opciones.Count < 99) ? 4 : 5
                :
                    (opciones.Count < 10)
                    ?
                        3
                    :
                        (opciones.Count < 100) ? 4 : 5
            );
        }
    }

    /// <summary>
    /// Especifica constantes que definen un estilo de numeración.
    /// </summary>
    public enum EstiloDeNumeracion
    {
        /// <summary>
        /// El estilo minúscula-paréntesis.
        /// </summary>
        MinusculaParentesis,

        /// <summary>
        /// El estilo mayúscula-paréntesis.
        /// </summary>
        MayusculaParentesis,

        /// <summary>
        /// El estilo mayúscula-punto.
        /// </summary>
        MayusculaPunto,

        /// <summary>
        /// El estilo minúscula-punto.
        /// </summary>
        MinusculaPunto,

        /// <summary>
        /// El estilo número-paréntesis.
        /// </summary>
        NumeroParentesis,

        /// <summary>
        /// El estilo número-punto.
        /// </summary>
        NumeroPunto
    }
}
