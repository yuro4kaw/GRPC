using Grpc.Core;
using Store_1.Model;
using Store_1.Service.interfaces;

namespace Store_1.Service
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;

        public GreeterService(ILogger<GreeterService> logger, ISellerService sellerService, IProductService productService)
        {
            _logger = logger;
            _sellerService = sellerService;
            _productService = productService;
        }

        public override async Task<ProductResponse> GetProductDetails(ProductRequest request, ServerCallContext context)
        {

            try
            {
                Product product = await _productService.GetAsync(request.Name);

                    Seller seller = await _sellerService.GetAsync(product.sellerId);
                    return new ProductResponse
                    {
                        ProductName = product.productName,
                        Price = (double)product.price,
                    };
            }
            catch (KeyNotFoundException ex)
            {
                return new ProductResponse
                {
                    ProductName = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ProductResponse
                {
                    ProductName = "Сталася помилка: " + ex.Message
                };
            }
        }
    }
}