using System;

namespace Fly.Handler.Utilities
{
    /// <summary>
    /// 获取一个独一无二的Id
    /// </summary>
    public static class IdHelper
    {
        public static T Generate<T>()
        {
            var guid = Guid.NewGuid();
            var type = typeof(T);
            if (typeof(T) == typeof(string))
            {
                return (T)(object)guid.ToString("N").ToUpper();
            }
            if (typeof(T) == typeof(byte[]))
            {
                var buffer = guid.ToByteArray();
                return (T)Convert.ChangeType(buffer, type);
            }
            throw new InvalidOperationException($"Operation [Generate<{type.Name}>] is not supported.");
        }
    }
}
