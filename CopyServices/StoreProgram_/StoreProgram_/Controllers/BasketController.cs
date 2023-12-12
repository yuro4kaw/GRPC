using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;
using StoreProgram_.Service.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace StoreProgram_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Basket>>> GetAll()
        {
            return Ok(await _basketService.GetAll());
        }

        [HttpGet("GetBasketById")]
        public async Task<ActionResult<IEnumerable<BasketResponse>>> GetByBasketId(int id)
        {
            return Ok(await _basketService.GetAsync(id));
        }

        [HttpPost("CreateNewBasket")]
        public async Task<ActionResult<List<Basket>>> CreateBasket([FromBody] BasketRequestcs basket)
        {
            await _basketService.InsertAsync(basket);
            return Ok(await _basketService.GetAll());
        }
        [HttpPost("CreateNewBasketGRPC")]
        public async Task<ActionResult<BasketResponse>> CreateBasketGrpc([FromBody] BasketRequestcs basket)
        {
            return Ok(await _basketService.InsertAsyncGrpc(basket));
        }
        [HttpPut("UpadateBasket")]
        public async Task<ActionResult<List<Basket>>> UpdateBasket(int id, [FromBody]BasketRequestcs request)
        {
            var basket = await _basketService.UpdateAsync(id, request);
            return Ok(await _basketService.GetAll());
        }
        [HttpDelete("DeleteBasket")]
        public async Task<ActionResult<List<Basket>>> Delete(int id)
        {
            await _basketService.DeleteAsync(id);
            return Ok(await _basketService.GetAll());
        }

        [HttpGet("GetJoinBasketWithClient")]
        public async Task<IActionResult> GetBasketWithClientInfo()
        {
            var basketWithClientInfo = await _basketService.GetBasketsWithClientInfo();
            return Ok(basketWithClientInfo);
        }
    }
}
