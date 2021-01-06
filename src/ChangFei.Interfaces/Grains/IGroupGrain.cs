using System.Threading.Tasks;

namespace ChangFei.Interfaces.Grains
{
    public interface IGroupGrain: IMessageSubscriber
    {
        Task SubscribeAsync(string userId, IGroupMessageSubscriber viewer);

        Task UnsubscribeAsync(string userId);
    }
}
