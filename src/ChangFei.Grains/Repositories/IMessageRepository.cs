using System.Threading.Tasks;
using ChangFei.Core.Message;

namespace ChangFei.Grains.Repositories
{
    public interface IMessageRepository
    {
        Task InsertAsync(Message message);
    }
}
