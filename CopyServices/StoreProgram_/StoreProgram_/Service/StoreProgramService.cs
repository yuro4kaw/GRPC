using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;
using StoreProgram_.Service.Interfaces;
using System;

namespace StoreProgram_.Service
{
    public class StoreProgramService : StoreProgram.StoreProgramBase
    {
        private readonly ILogger<StoreProgramService> _logger;
        private readonly IBasketService _basketService;
        private readonly IClientService _clientService;

        public StoreProgramService(ILogger<StoreProgramService> logger,IBasketService basketService, IClientService clientService)
        {
            _logger = logger;
            _basketService = basketService;
            _clientService = clientService;
        }

        public override async Task<BasketList> GetBaskets(Empty request, ServerCallContext context)
        {


            IEnumerable<BasketResponse> baskets = await _basketService.GetAll();

            BasketList productList = new BasketList();
            productList.Baskets.AddRange(baskets.Select(basket => new Basket1
            {
                BasketID = basket.BasketID,
                ClientID = basket.ClientID,
                ProductName = basket.ProductName,
                Quantity = basket.Quantity,
                Price = (double) basket.Price
            }));

            return productList;
        }
        public override async Task<ClientList> GetClients(Empty request, ServerCallContext context)
        {
            IEnumerable<ClientResponse> clients = await _clientService.GetAll();

            ClientList clientList = new ClientList();
            clientList.Clients.AddRange(clients.Select(client => new Client1
            {
                ClientID = client.ClientID,
                ClientName = client.ClientName,
                NumberPhone = client.NumberPhone
            }));

            return clientList;
        }
    }
}
