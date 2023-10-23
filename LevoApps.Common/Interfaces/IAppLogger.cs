using System;

namespace LevoApps.Common.Interfaces
{
    /// <summary>
    /// Contract interface for logging events in the application.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAppLogger<T>
    {
        /// <summary>
        /// Saves an event information message
        /// </summary>
        /// <param name="message"></param>
        void LogInformation(string message);

        /// <summary>
        /// Saves an event warning message
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);

        /// <summary>
        /// Saves an event critical error message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void LogError(string message, Exception ex = default!);

        /// <summary>
        /// Saves an event debug message
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);

        /// <summary>
        /// Saves an event trace message
        /// </summary>
        /// <param name="message"></param>
        void LogTrace(string message);
    }
}
