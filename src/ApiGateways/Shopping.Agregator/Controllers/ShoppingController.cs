using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Agregator.Models;
using Shopping.Agregator.Services;

namespace Shopping.Agregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            // Get basket with username
            var basket = await _basketService.GetBasketAsync(userName);
            // iterate basket items and consume products with basket item productId member
            foreach (var basketItem in basket.Items)
            {
                var product = await _catalogService.GetCatalogAsync(basketItem.ProductId);

                // map product related members into basketItem dto with extended columns
                // Set additional product fields onto basket item
                basketItem.ProductName = product.Name;
                basketItem.Catagory = product.Category;
                basketItem.Summary = product.Summary;
                basketItem.Description = product.Description;
                basketItem.ImageFile = product.ImageFile;
            }
            // consume ordering microservices in order to retrive order lsit
            var orders = await _orderService.GetOrdersByUserNameAsync(userName);

            // return root ShoppingModel dto class with include all response
            var shoppingModel = new ShoppingModel()
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return shoppingModel;
        }
    }
}
