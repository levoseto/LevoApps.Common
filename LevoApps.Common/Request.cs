namespace LevoApps.Common
{
    /// <summary>
    /// Request to be sent in case you wish to consult data with special information.
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
    /// Request to be sent in case you wish to consult data.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Client application consuming the service.
        /// </summary>
        public string? ClientApp { get; set; }
    }
}