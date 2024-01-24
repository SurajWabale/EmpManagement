using System.ComponentModel.DataAnnotations;

namespace EmpManagement.Models
{
    public class Login
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]    
        public string Phone {get; set; }
        [Required]
        public string LoginId { get; set; } 
        [Required]
        public string Password { get; set; }

        public Login()
        {
            
        }
    }
}
