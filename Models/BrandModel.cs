using System.ComponentModel.DataAnnotations;

namespace TechPhone.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập tên hãng:")]
        public string Name { get; set; }        
        public string Slug { get; set; }
        [Required(ErrorMessage = "Hãy nhập mô tả hãng:")]
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
