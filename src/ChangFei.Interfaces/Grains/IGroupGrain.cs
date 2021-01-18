using System.Threading.Tasks;

namespace ChangFei.Interfaces.Grains
{
    public interface IGroupGrain: IMessageSubscriber
    {
        Task SubscribeAsync(string userId, IMessageSubscriber viewer);

        Task UnsubscribeAsync(string userId);
    }
}
