namespace Fly.Handler.Channels
{
    public abstract class AbstractClientStatusHandler
    {
        public void SetOnline(ChannelContext context)
        {
            OnlineCallback(context);
        }

        public void SetOffline(ChannelContext context)
        {
            OfflineCallback(context);
        }

        protected abstract void OnlineCallback(ChannelContext context);
        protected abstract void OfflineCallback(ChannelContext context);
    }
}
