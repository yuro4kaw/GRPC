using Microsoft.Data.SqlClient;
using StoreProgram_.Model;
using StoreProgram_.Repository.Interfaces;
using System.Data;

namespace StoreProgram_.Repository
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
       private readonly DataContext _dataContext;

        public BasketRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public async Task<IEnumerable<Basket>> GetBasketsWithClientInfoAsync()
        {
            var basketsWithClientInfo = await _dataContext.Baskets
                .Include(basket => basket.Client)
                .ToListAsync();
            return basketsWithClientInfo;
        }
    }
}
