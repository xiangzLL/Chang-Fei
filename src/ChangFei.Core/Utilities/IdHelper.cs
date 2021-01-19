using System;

namespace ChangFei.Core.Utilities
{
    public static class IdHelper
    {
        /// <summary>
        /// Generate a unique Id.
        /// </summary>
        /// <returns></returns>
        public static T Generate<T>()
        {
            var guid = Guid.NewGuid();
            var type = typeof(T);
            if (typeof(T) == typeof(string))
            {
                return (T)(object)guid.ToString("N").ToUpper();
            }
            var buffer = guid.ToByteArray();
            if (typeof(T) == typeof(long))
            {
                var value = BitConverter.ToInt64(buffer, 0);
                return (T)Convert.ChangeType(value, type);
            }
            if (typeof(T) == typeof(byte[]))
            {
                return (T)Convert.ChangeType(buffer, type);
            }
            throw new InvalidOperationException($"Operation [Generate<{type.Name}>] is not supported.");
        }

    }
}
