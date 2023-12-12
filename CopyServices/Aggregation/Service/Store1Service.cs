using Grpc.Core;
using Aggregation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Aggregation.Model;
using Store_1;

namespace Aggregation.Service
{
   public class Store1Service : IStore1Service
   {

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var request = new Google.Protobuf.WellKnownTypes.Empty();
            var channel = GrpcChannel.ForAddress("https://localhost:7217");
            var client = new Store1.Store1Client(channel);
            var reply = await client.GetProductsAsync(request);

            IEnumerable<Product> productList = reply.Products
            .Select(product1 => new Product
            {
                id = product1.Id,
                productName = product1.ProductName,
                price = (decimal)product1.Price,
                description = product1.Description,
                sellerId = product1.SellerId
            }).ToList();

            return productList;
        }

        public async Task<IEnumerable<Seller>> GetAllSellers()
        {
            var request = new Google.Protobuf.WellKnownTypes.Empty();
            var channel = GrpcChannel.ForAddress("https://localhost:7217");
            var client = new Store1.Store1Client(channel);
            var reply = await client.GetSellerAsync(request);

            IEnumerable<Seller> productList = reply.Sellers
            .Select(seller1 => new Seller
            {
                id=seller1.Id,
                SellerName = seller1.SellerName,
                email = seller1.Email
            }).ToList();

            return productList;
        }
   }
}
