using System.ComponentModel.DataAnnotations;

namespace TechPhone.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Hãy nhập tên danh mục:")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Hãy nhập mô tả danh mục:")]
        public string Description { get; set; }
        public int Status { get; set; }

    }
}
