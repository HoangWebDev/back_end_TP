using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; //Tác vụ bất đông bộ
using Microsoft.EntityFrameworkCore;
using TechPhone.Models;
using TechPhone.Repository;
using TechPhone.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace TechPhone.Controllers
{
    [Authorize(Roles = "Admin")]    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;        
        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;            
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> GetProducts()
        {
            //Lấy danh sách tất cả sản phẩm
            return await _productRepository.GetAll();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            //Tìm sản phẩm theo id
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        //// PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductModel product)
        {
            //Kiểm tra nếu id không khớp vs id trong dữ liệu thì trả về lỗi
            if (id != product.Id)
            {
                return BadRequest();
            }

            //Tìm sản phẩm theo id
            var existingProduct = await _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }            


            // Cập nhật sản phẩm trong cơ sở dữ liệu
            await _productRepository.UpdateProduct(product, id);            

            return NoContent();
        }

        //// POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProduct(ProductModel product)
        {            
                await _productRepository.InsertProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);            
        }

        //// DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);            

            return NoContent();
        }
    }
}
