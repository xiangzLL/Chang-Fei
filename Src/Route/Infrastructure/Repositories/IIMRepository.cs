using System.Threading.Tasks;

namespace Route.Infrastructure.Repositories
{
    public interface IIMRepository
    {
        Task UpdateUserAddressAsync(int userId, string ipAddress);

        Task<string> GetUserAddressAsync(int id);
    }
}
