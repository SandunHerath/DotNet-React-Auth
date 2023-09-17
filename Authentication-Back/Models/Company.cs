using System.ComponentModel.DataAnnotations;

namespace AuthenticationOne.Models
{
    public class Company
    {
        [Key]
        public long ID { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        public string type { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
