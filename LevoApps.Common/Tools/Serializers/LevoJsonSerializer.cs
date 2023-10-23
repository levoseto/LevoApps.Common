using Microsoft.IO;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LevoApps.Common.Tools.Serializers
{
    /// <summary>
    /// Defines rules and methods for JSON operations in an application.
    /// </summary>
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
        /// Serialises a received object into a JSON string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>JSON Object in string</returns>
        public static string Serialize<T>(this T data)
        {
            if (data == null)
                return string.Empty;

            return JsonSerializer.Serialize(data, Options.Value);
        }

        /// <summary>
        /// Serialises a model to the content of an HttpRequestMessage.
        /// </summary>
        /// <typeparam name="T">Any object</typeparam>
        /// <param name="model">Object to be serialised in request</param>
        /// <param name="httpRequestMessage">Pre-built HttpRequestMessage with pre-information</param>
        /// <returns>Returns an HttpRequestMessage ready to be sent to a RESTfull Web API</returns>
        public static async Task<HttpRequestMessage> CrearJsonContent<T>(T model, HttpRequestMessage httpRequestMessage)
        {
            await using MemoryStream memory = new RecyclableMemoryStreamManager().GetStream();
            await JsonSerializer.SerializeAsync(memory, model, Options.Value).ConfigureAwait(false);
            memory.Position = 0;
            using var reader = new StreamReader(memory);
            string objectJson = await reader.ReadToEndAsync().ConfigureAwait(false);
            httpRequestMessage.Content = new StringContent(objectJson, Encoding.UTF8, JsonType);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(JsonType);
            return httpRequestMessage;
        }

        /// <summary>
        /// General method to deserialise an object of type T from a stream obtained from an HTTP response already sent from the REST API.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contentObtained"></param>
        /// <returns>Any Object deserialized</returns>
        public static async Task<T> DeserializeStreamToJson<T>(this HttpContent contentObtained)
        {
            await using Stream contentStream = await contentObtained.ReadAsStreamAsync().ConfigureAwait(false);
            return await JsonSerializer.DeserializeAsync<T>(contentStream, Options.Value).ConfigureAwait(false)
                ?? throw new NullReferenceException("The object could not be deserialised.");
        }

        /// <summary>
        /// General method to deserialise an object of type T from a fetched JSON string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cadenaOrigen"></param>
        /// <returns>Any object deserialized</returns>
        public static T DeserializeStringToJson<T>(this string stringOrigin)
        {
            return JsonSerializer.Deserialize<T>(stringOrigin, Options.Value)
                ?? throw new NullReferenceException("The object could not be deserialised.");
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