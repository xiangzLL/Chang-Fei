namespace Fly.Handler
{
    /// <summary>
    /// FlyClient的基类
    /// </summary>
    public abstract class BaseFlyClient
    {
        public const int UnAssignedChannelId = -1;

        /// <summary>
        /// 获取Client 的Id
        /// </summary>
        public string Id { get; }

        protected BaseFlyClient()
        {
            Id = new FlyClientIdContext().Id;
        }
    }
}
