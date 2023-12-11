using Grpc.Core;
using GrpcServer;

namespace GrpcServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task<ProductResponse> GetProductDetails(ProductRequest request, ServerCallContext context)
        {

                return await Task.FromResult(new ProductResponse
                {
                    ProductName = "Som",
                    Price = 12.0
                });
        }
    }
}