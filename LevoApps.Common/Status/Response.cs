using System;
using System.Collections.Generic;

namespace LevoApps.Common.Status
{
    /// <summary>
    /// Comparte el estado de operación de un método que devuelva un resultado.
    /// </summary>
    public class Response<T> : Response
    {
        /// <summary>
        /// Devuelve los datos que se requieran proporcionar.
        /// </summary>
        public T Data { get; set; } = default!;

        /// <summary>
        /// Devuelve un resultado satisfactorio de un método con su información proporcionada.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Resultado satisfactorio</returns>
        public static Response<T> Successful(T data, string message, IList<string>? messages = null)
        {
            return new Response<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                Messages = messages
            };
        }

        /// <summary>
        /// Devuelve un resultado neutro de una operación que no tiene ningún estado safistactorio.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Resultado neutro</returns>
        public new static Response<T> Nothing(string message, IList<string>? messages = null)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Messages = messages
            };
        }

        /// <summary>
        /// Devuelve un resultado fallido con posible excepción obtenida.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Resultado de error</returns>
        public new static Response<T> Fail(Exception? exception, string message = "", IList<string>? messages = null)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Exception = exception,
                Message = message,
                Messages = messages
            };
        }
    }

    /// <summary>
    /// Comparte el estado de operación de un método.
    /// </summary>
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IList<string>? Messages { get; set; }
        public Exception? Exception { get; set; }

        /// <summary>
        /// Devuelve un resultado satisfactorio de un método.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Resultado satisfactorio</returns>
        public static Response Successful(string message, IList<string>? messages = null)
        {
            return new Response { IsSuccess = true, Message = message, Messages = messages };
        }

        /// <summary>
        /// Devuelve un resultado neutro de una operación que no tiene ningún estado safistactorio.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Resultado neutro</returns>
        public static Response Nothing(string message, IList<string>? messages = null)
        {
            return new Response { IsSuccess = false, Message = message, Messages = messages };
        }

        /// <summary>
        /// Devuelve un resultado fallido con posible excepción obtenida.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Resultado de error</returns>
        public static Response Fail(Exception? exception, string message = "", IList<string>? messages = null)
        {
            return new Response
            {
                IsSuccess = false,
                Exception = exception,
                Message = message,
                Messages = messages
            };
        }
    }
}