namespace Forconsol
{
    /// <summary>
    /// Representa una animación o mensaje de carga pensado para ser usado para operaciones que toman tiempo en completarse.
    /// </summary>
    public class MensajeDeCarga
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MensajeDeCarga"/>.
        /// </summary>
        public MensajeDeCarga()
        {
        
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MensajeDeCarga"/> con un mensaje especificado.
        /// </summary>
        /// <param name="mensaje">Un mensaje pensado para avisarle al usuario algo.</param>
        public MensajeDeCarga(string? mensaje)
        {
            Mensaje = mensaje;
        }

        private Thread animacion;
        private bool estaActivo;

        /// <summary>
        /// Obtiene o establece el estilo del mensaje de carga.
        /// </summary>
        /// <returns>Una <see cref="EstiloDeCarga"/> de la instancia actual. La predeterminada es puntos.</returns>
        public EstiloDeCarga EstiloDeCarga { get; set; }

        /// <summary>
        /// Obtiene o establece el mensaje del mensaje de carga, o null si ninguno fue suministrado.
        /// </summary>
        /// <returns>Una <see cref="string"/> pensada para avisarle al usuario algo.</returns>
        public string? Mensaje { get; set; }

        /// <summary>
        /// Obtiene un valor que indica si este mensaje de carga está activo.
        /// </summary>
        /// <returns><see langword="true"/> si la animación asíncrona de este mensaje de carga está activa; <see langword="false"/> en caso contrario.</returns>
        public bool EstaActivo { get => estaActivo; }

        private void AccionLineaRotatoria()
        {
            if (Mensaje != null)
                Console.Write(' ');

            Console.Write('─');

            while (true)
            {
                try
                {
                    Thread.Sleep(1_000);
                    Console.Write("\b\\");
                    Thread.Sleep(1_000);
                    Console.Write("\b|");
                    Thread.Sleep(1_000);
                    Console.Write("\b/");
                    Thread.Sleep(1_000);
                    Console.Write("\b─");
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
            }
        }

        private void AccionLineas()
        {
            if (Mensaje != null)
                Console.Write(' ');

            Console.Write("___");

            while (true)
            {
                try
                {
                    Thread.Sleep(1_000);
                    Console.Write("\b\b\b-__");
                    Thread.Sleep(1_000);
                    Console.Write("\b\b\b_-_");
                    Thread.Sleep(1_000);
                    Console.Write("\b\b_-");
                    Thread.Sleep(1_000);
                    Console.Write("\b_");
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
            }
        }

        private void AccionPuntos()
        {
            Console.Write("   ");

            while (true)
            {
                try
                {
                    Thread.Sleep(1_000);
                    Console.Write("\b\b\b.  ");
                    Thread.Sleep(1_000);
                    Console.Write("\b\b. ");
                    Thread.Sleep(1_000);
                    Console.Write("\b.");
                    Thread.Sleep(1_000);
                    Console.Write("\b\b\b   ");
                }
                catch (ThreadInterruptedException)
                {
                    break;
                }
            }
        }

        private void Iniciar()
        {
            Console.Write(Mensaje);

            animacion = new(EstiloDeCarga switch
            {
                EstiloDeCarga.LineaRotatoria => AccionLineaRotatoria,
                EstiloDeCarga.Lineas => AccionLineas,
                _ => AccionPuntos
            });

            animacion.Start();
        }

        /// <summary>
        /// <para>
        /// Inicia el mensaje de carga o detiene su animación asíncrona, de acuerdo a la <see cref="EstaActivo"/> propiedad.
        /// </para>
        /// <para>
        /// Si <see cref="EstaActivo"/> es <see langword="false"/>, entonces este método imprime el <see cref="Mensaje"/> e inicia una animación asíncrona.
        /// </para>
        /// <para>
        /// Si <see cref="EstaActivo"/> es <see langword="true"/>, entonces este método detiene la animación asíncrona de esta instancia.
        /// </para>
        /// </summary>
        public void Alternar()
        {
            if (estaActivo)
            {
                animacion.Interrupt();
                Console.WriteLine();

                estaActivo = false;
            }
            else
            {
                Iniciar();

                estaActivo = true;
            }
        }
    }

    /// <summary>
    /// Especifica constantes que definen un estilo de carga.
    /// </summary>
    public enum EstiloDeCarga
    {
        /// <summary>
        /// El estilo puntos.
        /// </summary>
        Puntos,

        /// <summary>
        /// El estilo línea rotatoria.
        /// </summary>
        LineaRotatoria,

        /// <summary>
        /// El estilo líneas.
        /// </summary>
        Lineas
    }
}
