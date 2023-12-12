using Store_1.Model;

namespace Store_1.Service.interfaces
{
    public interface ISellerService
    {
        Task<Seller> GetAsync(int id);
        Task<List<Seller>> GetAllAsync();
    }
}
