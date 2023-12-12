using StoreProgram_.Model;

namespace StoreProgram_.Repository.Interfaces
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        Task<IEnumerable<Basket>> GetBasketsWithClientInfoAsync();
    }
}
