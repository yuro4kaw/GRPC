using Aggregation.Model;

namespace Aggregation.Service
{
    public interface IStoreProgramService
    {
        public Task<IEnumerable<Client>> GetAllClients();
        public Task<IEnumerable<Basket>> GetAllBaskets();
    }
}
