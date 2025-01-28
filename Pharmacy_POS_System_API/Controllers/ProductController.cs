using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_POS_System_API.Data;
using Pharmacy_POS_System_API.Models;

namespace Pharmacy_POS_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }


        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            var products = await _context.Products
                                          .Where(p => p.CategoryId == categoryId)
                                          .Include(p => p.Category)
                                          .ToListAsync();

            if (products == null || !products.Any())
            {
                return NotFound("No products found for the specified category.");
            }

            return Ok(products);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        //{

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound($"Product with ID {id} not found.");
        //    }

        //    product.Name = productDto.Name;
        //    product.Price = productDto.Price;
        //    product.Barcode = productDto.Barcode;
        //    product.Generic = productDto.Generic;
        //    product.Discount = productDto.Discount;
        //    product.Vat = productDto.Vat;
        //    product.ImageUrl = productDto.ImageUrl;
        //    product.StockQuantity = productDto.StockQuantity;
        //    product.CategoryId = productDto.CategoryId;

        //    await _context.SaveChangesAsync();

        //    return Ok(product); 
        //}


        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromForm] ProductDto productCreateDto)
        {
            var category = await _context.Categories.FindAsync(productCreateDto.CategoryId);
            if (category == null)
            {
                return NotFound($"Category with ID {productCreateDto.CategoryId} not found.");
            }

            byte[] imageData = null;

            if (productCreateDto.Image != null && productCreateDto.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productCreateDto.Image.CopyToAsync(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            var product = new Product
            {
                Name = productCreateDto.Name,
                Price = productCreateDto.Price,
                Barcode = productCreateDto.Barcode,
                Generic = productCreateDto.Generic,
                Discount = productCreateDto.Discount,
                Vat = productCreateDto.Vat,
                Image = imageData, 
                StockQuantity = productCreateDto.StockQuantity,
                CategoryId = productCreateDto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetProductImage(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.Image == null)
            {
                return NotFound("Image not found.");
            }

            return File(product.Image, "image/jpeg"); 
        }



    }
}
