using System.Threading.Tasks;
using ChangFei.Core.Message;

namespace ChangFei.Grains.Repositories
{
    public interface IIMDataRepository
    {
        Task InsertData(Message message);
    }
}
