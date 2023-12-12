using Aggregation.Model;

namespace Aggregation.Service
{
    public interface IStore1Service
    {
        public Task<IEnumerable<Product>> GetAllProducts();
        public Task<IEnumerable<Seller>> GetAllSellers();
    }
}
