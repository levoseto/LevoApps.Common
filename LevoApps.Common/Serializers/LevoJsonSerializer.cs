using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.IO;

namespace LevoApps.Common.Serializers
{
    public static class LevoJsonSerializer
    {
        private const string JsonType = "application/json";

        public static readonly Lazy<JsonSerializerOptions> Options = new Lazy<JsonSerializerOptions>(JsonSerializerOptions);

        /// <summary>
        /// JsonOptions
        /// </summary>
        public static JsonSerializerOptions JsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                Converters = {
                    new JsonStringEnumConverter(),
                    new TimeSpanConverter()
                },
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = null,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
        }

        /// <summary>
        /// Serializa un objeto recibido en una cadena JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>Cadena objeto</returns>
        public static string Serializar<T>(this T data)
        {
            if (data == null)
                return string.Empty;

            return JsonSerializer.Serialize(data, Options.Value);
        }

        /// <summary>
        /// Serializa un modelo al content de un HttpRequestMessage.
        /// </summary>
        /// <typeparam name="T">Objeto cualquiera</typeparam>
        /// <param name="modelo">Objeto a serializar en request</param>
        /// <param name="uri">URL de Web API a donde se realizará el request</param>
        /// <param name="applicationHeader">Header de tipo de aplicación y serialización a utilizar</param>
        /// <param name="httpMethod">Tipo de predicado del método que usará el Web API para procesar</param>
        /// <returns></returns>
        public static async Task<HttpRequestMessage> CrearJsonContent<T>(T modelo, HttpRequestMessage httpRequestMessage)
        {
            await using MemoryStream memory = new RecyclableMemoryStreamManager().GetStream();
            await JsonSerializer.SerializeAsync(memory, modelo, Options.Value).ConfigureAwait(false);
            memory.Position = 0;
            using var reader = new StreamReader(memory);
            string objectJson = await reader.ReadToEndAsync().ConfigureAwait(false);
            httpRequestMessage.Content = new StringContent(objectJson, Encoding.UTF8, JsonType);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(JsonType);
            return httpRequestMessage;
        }

        /// <summary>
        /// Método general para deserializar un objeto de tipo T desde un stream obtenido de una respuesta HTTP ya enviada desde el REST API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="respuestaObtenida"></param>
        /// <returns></returns>
        public static async Task<T> DeserializeStreamToJson<T>(this HttpContent respuestaObtenida)
        {
            await using Stream contentStream = await respuestaObtenida.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<T>(contentStream, Options.Value).ConfigureAwait(false)
                ?? throw new NullReferenceException("No se pudo deserializar el objeto.");
        }

        /// <summary>
        /// Método general para deserializar un objeto de tipo T desde un string JSON obtenido.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="respuestaObtenida"></param>
        /// <returns></returns>
        public static T DeserializeStringToJson<T>(this string cadenaOrigen)
        {
            return JsonSerializer.Deserialize<T>(cadenaOrigen, Options.Value)
                ?? throw new NullReferenceException("No se pudo deserializar el objeto.");
        }

        /// <summary>
        /// for compatibility with older UI versions ( <3.0 ) we arrange
        /// timespan serialization as s
        /// </summary>
        /// <seealso cref="JsonConverter&lt;TimeSpan&gt;" />
        public class TimeSpanConverter : JsonConverter<TimeSpan>
        {
            /// <summary>Reads and converts the JSON to type <typeparamref name="T" />.</summary>
            /// <param name="reader">The reader.</param>
            /// <param name="typeToConvert">The type to convert.</param>
            /// <param name="options">An object that specifies serialization options to use.</param>
            /// <returns>The converted value.</returns>
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return TimeSpan.Parse(reader.GetString());
            }

            /// <summary>Writes a specified value as JSON.</summary>
            /// <param name="writer">The writer to write to.</param>
            /// <param name="value">The value to convert to JSON.</param>
            /// <param name="options">An object that specifies serialization options to use.</param>
            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("c"));
            }
        }
    }
}