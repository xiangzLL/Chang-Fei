using System.Threading.Tasks;

namespace ChangFei.Interfaces.Grains
{
    /// <summary>
    /// User Grain
    /// </summary>
    public interface IUserGrain:IMessageSender, IMessageSubscriber
    {
        Task LoginAsync(IMessageViewer viewer);

        Task LogoutAsync();
    }
}
