using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_POS_System_API.Models;
using Pharmacy_POS_System_API.Session;
using System.Net.Http;

namespace Pharmacy_POS_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly string CartSessionKey = "Cart";
        [HttpGet]
        public ActionResult<List<CartItem>> GetCart()
        {

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartSessionKey") ?? new List<CartItem>();
            return Ok(cart);
        }

        [HttpPost("AddToCart")]
        public ActionResult AddToCart(CartItem item)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartSessionKey") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.SubTotal = existingItem.Quantity * existingItem.Price;
            }
            else
            {
                item.SubTotal = item.Quantity * item.Price;
                cart.Add(item);
            }

            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            return Ok(cart);
        }

        [HttpDelete("RemoveFromCart/{productId}")]
        public ActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CartSessionKey") ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null) cart.Remove(item);

            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            return Ok(cart);
        }
    }
}
