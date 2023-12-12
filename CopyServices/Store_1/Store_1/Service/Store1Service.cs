using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Store_1.Service.interfaces;
using Store_1.Model;

namespace Store_1.Service
{
    public class Store1Service : Store1.Store1Base
    {
        private readonly ILogger<Store1Service> _logger;
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;

        public Store1Service(ILogger<Store1Service> logger, ISellerService sellerService, IProductService productService)
        {
            _logger = logger;
            _sellerService = sellerService;
            _productService = productService;
        }

        public override async Task<ProductList> GetProducts(Empty request, ServerCallContext context)
        {


            List<Product> product = await _productService.GetAllAsync();

            ProductList productList = new ProductList();
            productList.Products.AddRange(product.Select(product => new Product1
            {
                Id = product.id,
                ProductName = product.productName,
                Price = (double)product.price,
                Description = product.description,
                SellerId = product.sellerId
            }));

            return productList;
        }
        public override async Task<SellerList> GetSeller(Empty request, ServerCallContext context)
        {
            List<Seller> sellers = await _sellerService.GetAllAsync();

            SellerList sellerList = new SellerList();
            sellerList.Sellers.AddRange(sellers.Select(seller => new Seller1
            {
                Id = seller.id,
                SellerName = seller.SellerName,
                Email = seller.email
            }));

            return sellerList;
        }
    }
}

