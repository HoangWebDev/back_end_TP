using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechPhone.Models;
using TechPhone.Repository;

namespace TechPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly DataContext _context;

        public BrandController (DataContext context)
        {
            _context = context;
        }

        //Get: api/Brand
        //Sử dụng ActionResult vì cần trả về dữ liệu BrandModel hoặc trạng thái
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandModel>>> GetBrands()
        {
            return await _context.Brands.ToListAsync();
        }

        //Get: api/Brand/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandModel>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return brand;
        }

        //Put: api/Brand/{id}
        //Sử dụng IActionResult vì chỉ cần trả về trạng thái thành công hoặc không
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, BrandModel brand)
        {
            //Kiểm tra id
            if (id != brand.Id)
            {
                return BadRequest();
            }

            //TÌm hãng
            var existingBrand = await _context.Brands.FindAsync(id);
            if (existingBrand == null)
            {
                return NotFound();
            }

            //Cập nhật các thuộc tính của hãng
            existingBrand.Name = brand.Name;
            existingBrand.Slug = brand.Slug;
            existingBrand.Description = brand.Description;
            existingBrand.Status = brand.Status;

            //Lưu vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //Post: api/Brand
        [HttpPost]
        public async Task<ActionResult<BrandModel>> PostBrand(BrandModel brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new {id = brand.Id}, brand);
        }

        //Delete: api/Brand/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {                 
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
