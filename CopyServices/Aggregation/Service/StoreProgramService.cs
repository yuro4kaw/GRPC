using Aggregation.Model;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using StoreProgram_;

namespace Aggregation.Service
{
    public class StoreProgramService:IStoreProgramService
    {
        public async Task<IEnumerable<Basket>> GetAllBaskets()
        {
            var request = new Google.Protobuf.WellKnownTypes.Empty();
            var channel = GrpcChannel.ForAddress("https://localhost:5051");
            var client = new StoreProgram.StoreProgramClient(channel);
            var reply = await client.GetBasketsAsync(request);

            IEnumerable<Basket> productList = reply.Baskets
            .Select(basket1 => new Basket
            {
                BasketID = basket1.BasketID,
                ClientID = basket1.ClientID,
                ProductName = basket1.ProductName,
                Quantity = basket1.Quantity,
                Price = (decimal)basket1.Price
            }).ToList();

            return productList;
        }

        public async Task<IEnumerable<Client>> GetAllClients()
        {
            var request = new Google.Protobuf.WellKnownTypes.Empty();
            var channel = GrpcChannel.ForAddress("https://localhost:5051");
            var client = new StoreProgram.StoreProgramClient(channel);
            var reply = await client.GetClientsAsync(request);

            IEnumerable<Client> productList = reply.Clients
            .Select(client1 => new Client
            {
                ClientID = client1.ClientID,
                ClientName = client1.ClientName,
                NumberPhone = client1.NumberPhone
            }).ToList();

            return productList;
        }
    }
}

