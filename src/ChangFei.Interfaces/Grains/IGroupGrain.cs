using System.Threading.Tasks;

namespace ChangFei.Interfaces.Grains
{
    public interface IGroupGrain: IMessageSender, IMessageSubscriber
    {
    }
}
