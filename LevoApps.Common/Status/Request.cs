namespace LevoApps.Common.Status
{
    /// <summary>
    /// Solicitud a enviar en caso de que se desee consultar datos con información especial.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Request<T> : Request
    {
        /// <summary>
        /// Datos a consultar.
        /// </summary>
        public T Data { get; set; } = default!;
    }

    /// <summary>
    /// Solicitud a enviar en caso de que se desee consultar datos.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Cliente que consume el servicio.
        /// </summary>
        public string? DescripcionCliente { get; set; }
    }
}