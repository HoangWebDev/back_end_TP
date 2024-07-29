using System.ComponentModel.DataAnnotations;

namespace TechPhone.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }        

        public string Role { get; set; }
    }
}
