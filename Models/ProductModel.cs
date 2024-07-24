using System.ComponentModel.DataAnnotations;

namespace TechPhone.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập tên sản phẩm:")]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required(ErrorMessage = "Hãy nhập mô tả sản phẩm:")]
        public string Description { get; set; }        
        [Required(ErrorMessage = "Hãy nhập giá sản phẩm")]
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }       
        public string Image {  get; set; }
    }
}
