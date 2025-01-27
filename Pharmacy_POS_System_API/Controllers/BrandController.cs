using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy_POS_System_API.Data;
using Pharmacy_POS_System_API.Models;

namespace Pharmacy_POS_System_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            var brands=  await _context.Brands.ToListAsync();

            return Ok(brands);
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> AddBrand(BrandDto brandDto)
        {
            var brand = new Brand
            {
                Name = brandDto.Name
            };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrands), new { id = brand.Id }, brand);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands
                                      .Include(b => b.Categories)
                                      .FirstOrDefaultAsync(b => b.Id == id);

            if (brand == null)
            {
                return NotFound($"Brand with ID {id} not found.");
            }


            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent(); 
        }
    }
}
