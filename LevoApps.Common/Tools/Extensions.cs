using System;

namespace LevoApps.Common.Tools
{
    /// <summary>
    /// Crea métodos adecuados para validaciones y modificaciones en valores correctos o de presentación
    /// </summary>
    public static class Extensions
    {
        #region Primitive validations

        public static bool IsValid(this string value)
        {
            return !string.IsNullOrEmpty(value) || !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValid(this DateTime value)
        {
            return value != default;
        }

        public static bool IsValid(this DateTimeOffset value)
        {
            return value != default;
        }

        public static bool IsValid(this TimeSpan value)
        {
            return value != default;
        }

        public static bool IsValid(this byte value)
        {
            return value != 0;
        }

        public static bool IsValid(this short value)
        {
            return value != 0;
        }

        public static bool IsValid(this int value)
        {
            return value != 0;
        }

        public static bool IsValid(this long value)
        {
            return value != 0;
        }

        public static bool IsValid(this sbyte value)
        {
            return value != 0;
        }

        public static bool IsValid(this ushort value)
        {
            return value != 0;
        }

        public static bool IsValid(this uint value)
        {
            return value != 0;
        }

        public static bool IsValid(this ulong value)
        {
            return value != 0;
        }

        public static bool IsValid(this float value)
        {
            return value != 0;
        }

        public static bool IsValid(this double value)
        {
            return value != 0;
        }

        public static bool IsValid(this decimal value)
        {
            return value != 0;
        }

        #endregion

        #region Primitive format

        public static string Format(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static string FormatCurrency(this float value)
        {
            return value.ToString("c");
        }

        public static string FormatCurrency(this double value)
        {
            return value.ToString("c");
        }

        public static string FormatCurrency(this decimal value)
        {
            return value.ToString("c");
        }

        public static double Truncate(this float value)
        {
            return Math.Truncate(value);
        }

        public static double Round(this float value, byte precision)
        {
            return Math.Round(value, precision);
        }

        public static double Round(this double value, byte precision)
        {
            return Math.Round(value, precision);
        }

        #endregion
    }
}
