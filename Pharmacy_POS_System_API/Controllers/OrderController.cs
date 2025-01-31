using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_POS_System_API.Data;
using Pharmacy_POS_System_API.Models;

namespace Pharmacy_POS_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("save-order")]
        public async Task<IActionResult> SaveOrder([FromBody] OrderDto orderDto)
        {
            var order = new Order
            {
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                Discount = orderDto.Discount,
                Vat = orderDto.Vat,
                PaidAmount = orderDto.PaidAmount,
                ChangeAmount = orderDto.ChangeAmount,
                PaymentMethod = orderDto.PaymentMethod,
                Items = orderDto.Items.Select(i => new CartItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    ProductName=i.ProductName,
                    Price = i.Price,
                    Discount=i.Discount,
                    Vat=i.Vat,
                    SubTotal = i.SubTotal
                }).ToList()
            };

            _context.Orders.Add(order);

            foreach (var item in orderDto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    if (product.StockQuantity >= item.Quantity)
                    {
                        product.StockQuantity -= item.Quantity;
                    }
                    else
                    {
                        return BadRequest($"Not enough stock for product {product.Name}");
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Order Saved Successfully!" });
        }

        [HttpGet("get-all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Items) 
                .Select(o => new Order
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Discount = o.Discount,
                    Vat = o.Vat,
                    PaidAmount = o.PaidAmount,
                    ChangeAmount = o.ChangeAmount,
                    PaymentMethod = o.PaymentMethod,
                    Items = o.Items.Select(i => new CartItem
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        ProductName = i.ProductName,
                        Price = i.Price,
                        Discount = i.Discount,
                        Vat = i.Vat,
                        SubTotal = i.SubTotal
                    }).ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }
    }

}


