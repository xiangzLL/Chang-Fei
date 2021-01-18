using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Grains.Entity;

namespace ChangFei.Grains.Repositories
{
    public interface IMessageRepository
    {
        Task InsertAsync(MessageRecord messageRecord);
    }
}
