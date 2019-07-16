using Fly.Handler.Utilities;

namespace Fly.Handler
{
    internal class FlyClientIdContext
    {
        /// <summary>
        /// 全局Context Id
        /// </summary>
        public string Id { get; }

        public FlyClientIdContext()
        {
            Id = IdHelper.Generate<string>();
        }
    }
}
