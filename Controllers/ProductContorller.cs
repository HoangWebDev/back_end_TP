using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; //Tác vụ bất đông bộ
using Microsoft.EntityFrameworkCore;
using TechPhone.Models;
using TechPhone.Repository;

namespace TechPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context; //Thêm vàu DataContext qua contructor
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            //Lấy danh sách tất cả sản phẩm
            return await _context.Products                                        
                                        .ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            //Tìm sản phẩm theo id
            var product = await _context.Products                                  
                .FindAsync(id);

            if (product == null)
            {
                //Nếu không có báo lỗi 404
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductModel product)
        {
            //Kiểm tra nếu id không khớp vs id trong dữ liệu thì trả về lỗi
            if (id != product.Id)
            {
                return BadRequest();
            }

            //Tìm sản phẩm theo id
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Cập nhật các thuộc tính của sản phẩm
            existingProduct.Name = product.Name;
            existingProduct.Slug = product.Slug;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.BrandId = product.BrandId;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Image = product.Image;

            // Kiểm tra xem BrandId và CategoryId có tồn tại không
            var brandExists = await _context.Brands.AnyAsync(b => b.Id == product.BrandId);
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);

            if (!brandExists || !categoryExists)
            {
                return BadRequest("Invalid Brand or Category");
            }

            //Lưu vào cơ sở dữ liệu
            await _context.SaveChangesAsync();          

            return NoContent();
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel product)
        {
            // Kiểm tra xem BrandId và CategoryId có tồn tại không
            var brandExists = await _context.Brands.AnyAsync(b => b.Id == product.BrandId);
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == product.CategoryId);

            if (!brandExists || !categoryExists)
            {
                return BadRequest("Invalid Brand or Category");
            }

                _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }      
    }
}
