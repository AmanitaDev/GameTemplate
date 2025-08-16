using System;
using System.Collections.Generic;

namespace GameTemplate.Scripts.Utils
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get all values of an enum as a list.
        /// </summary>
        public static List<T> GetValues<T>() where T : Enum
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }

        /// <summary>
        /// Get all names of an enum as a list.
        /// </summary>
        public static List<string> GetNames<T>() where T : Enum
        {
            return new List<string>(Enum.GetNames(typeof(T)));
        }

        /// <summary>
        /// Parse a string to an enum value safely.
        /// </summary>
        public static T ParseEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum
        {
            if (Enum.TryParse<T>(value, ignoreCase, out var result))
            {
                return result;
            }

            throw new ArgumentException($"Cannot convert '{value}' to enum {typeof(T).Name}");
        }

        /// <summary>
        /// Check if an enum value exists in the enum type.
        /// </summary>
        public static bool IsDefined<T>(this T value) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value);
        }

        /// <summary>
        /// Get the next enum value (loops back to first if at the end).
        /// </summary>
        public static T Next<T>(this T value) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            int index = Array.IndexOf(values, value);
            index = (index + 1) % values.Length;
            return values[index];
        }

        /// <summary>
        /// Get the previous enum value (loops back to last if at the beginning).
        /// </summary>
        public static T Previous<T>(this T value) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            int index = Array.IndexOf(values, value);
            index = (index - 1 + values.Length) % values.Length;
            return values[index];
        }
    }
}