using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aggregation.Model;
using Aggregation.Services;
using Aggregation.Service;
using Microsoft.VisualBasic;

namespace Aggregation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStore_1Service _store_1Service;
        private readonly IStoreProgram_Service _storeProgram_Service;
        private readonly IStore1Service _store1Service;
        private readonly IStoreProgramService _storeProgramService;

        public StoreController(IStore_1Service store_1Service, IStoreProgram_Service storeProgram_Service,
            IStoreProgramService storeProgramService, IStore1Service store1Service)
        {
            _store_1Service = store_1Service;
            _storeProgram_Service = storeProgram_Service;
            _store1Service=store1Service;
            _storeProgramService=storeProgramService;
        }
        [HttpGet("GetAll")]
        public async Task<GetAllModel> GetAll()
        {
            var products = await _store_1Service.GetProducts();
            var sellers = await _store_1Service.GetSeller();
            var baskets = await _storeProgram_Service.GetBaskets();
            var clients = await _storeProgram_Service.GetClients();

            var result = new GetAllModel
            {
                Product = products,
                Seller = sellers,
                Basket = baskets,
                Client = clients
            };

            return result;
        }
        [HttpGet("GetAllGRPC")]
        public async Task<GetAllModel> GetALLGRPC()
        {
            var products = await _store1Service.GetAllProducts();
            var sellers = await _store1Service.GetAllSellers();
            var baskets = await _storeProgramService.GetAllBaskets();
            var clients = await _storeProgramService.GetAllClients();

            var result = new GetAllModel
            {
                Product = products,
                Seller = sellers,
                Basket = baskets,
                Client = clients
            };

            return result;
        }
    }
}
