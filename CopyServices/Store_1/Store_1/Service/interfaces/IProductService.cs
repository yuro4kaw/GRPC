using Store_1.Model;

namespace Store_1.Service.interfaces
{
    public interface IProductService
    {
        Task<Product> GetAsync(string name);
        Task<List<Product>> GetAllAsync();
    }
}
