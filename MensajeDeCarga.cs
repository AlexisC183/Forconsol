namespace Forconsol
{
    /// <summary>
    /// Representa una animación o mensaje de carga pensado para ser usado para operaciones que toman tiempo en completarse.
    /// </summary>
    public class MensajeDeCarga
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MensajeDeCarga"/> con un mensaje especificado.
        /// </summary>
        /// <param name="mensaje">Un mensaje pensado para avisarle al usuario algo.</param>
        public MensajeDeCarga(string? mensaje)
        {
            this.mensaje = mensaje;
            fuente = new();
        }

        private CancellationTokenSource fuente;
        private string mensaje;

        /// <summary>
        /// Obtiene o establece el estilo del mensaje de carga.
        /// </summary>
        /// <returns>Una <see cref="EstiloDeCarga"/> de la instancia actual. La predeterminada es puntos.</returns>
        public EstiloDeCarga EstiloDeCarga { get; set; }

        private async void AccionLineaRotatoria()
        {
            if (mensaje != null)
            {
                Console.Write(' ');
            }
            Console.Write('─');
            while (true)
            {
                try
                {
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\\");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b|");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b/");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b─");
                }
                catch (TaskCanceledException)
                {

                }
            }
        }

        private async void AccionLineas()
        {
            if (mensaje != null)
            {
                Console.Write(' ');
            }
            Console.Write("___");
            while (true)
            {
                try
                {
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\b\b-__");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\b\b_-_");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\b_-");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b_");
                }
                catch (TaskCanceledException)
                {

                }
            }
        }

        private async void AccionPuntos()
        {
            Console.Write("   ");
            while (true)
            {
                try
                {
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\b\b.  ");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\b. ");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b.");
                    await Task.Delay(1000, fuente.Token);
                    Console.Write("\b\b\b   ");
                }
                catch (TaskCanceledException)
                {

                }
            }
        }

        /// <summary>
        /// Inicia el mensaje de carga imprimiendo dicho mensaje e iniciando una animación asíncrona.
        /// </summary>
        public void Start()
        {
            Task tarea;

            Console.Write(mensaje);
            switch (EstiloDeCarga)
            {
                case EstiloDeCarga.LineaRotatoria:
                    tarea = new(AccionLineaRotatoria, fuente.Token);
                    break;
                case EstiloDeCarga.Lineas:
                    tarea = new(AccionLineas, fuente.Token);
                    break;
                default:
                    tarea = new(AccionPuntos, fuente.Token);
                    break;
            }
            tarea.Start();
        }

        /// <summary>
        /// Detiene la animación asíncrona del mensaje de carga.
        /// </summary>
        public void Stop()
        {
            fuente.Cancel();
            Console.WriteLine();
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
