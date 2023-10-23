using System;
using System.Collections.Generic;

namespace LevoApps.Common
{
    /// <summary>
    /// Shares the operation state of a method that returns a result.
    /// </summary>
    public class Response<T> : Response
    {
        /// <summary>
        /// Returns the data required to be provided.
        /// </summary>
        public T Data { get; set; } = default!;

        /// <summary>
        /// Returns a successful result of a method with its provided information.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Successful response</returns>
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
        /// Returns a neutral result of an operation that has no safest state.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Neutral response</returns>
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
        /// Returns a failed result with possible exception raised.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Error response</returns>
        public static new Response<T> Fail(string message = "", Exception? exception = null, IList<string>? messages = null)
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
    /// Shares the operational status of a method.
    /// </summary>
    public class Response
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public IList<string>? Messages { get; set; }
        public Exception? Exception { get; set; }

        /// <summary>
        /// Returns a successful result of a method.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Successful response</returns>
        public static Response Successful(string message, IList<string>? messages = null)
        {
            return new Response { IsSuccess = true, Message = message, Messages = messages };
        }

        /// <summary>
        /// Returns a neutral result of an operation that has no safest state.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Neutral response</returns>
        public static Response Nothing(string message, IList<string>? messages = null)
        {
            return new Response { IsSuccess = false, Message = message, Messages = messages };
        }

        /// <summary>
        /// Returns a failed result with possible exception raised.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messages"></param>
        /// <returns>Error response</returns>
        public static Response Fail(string message = "", Exception? exception = null, IList<string>? messages = null)
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