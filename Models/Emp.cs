using System.ComponentModel.DataAnnotations;

namespace EmpManagement.Models
{
    public class Emp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public long salary { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }

        public Emp()
        {
            
        }


    }
}
