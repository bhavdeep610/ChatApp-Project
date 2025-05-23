using System.ComponentModel.DataAnnotations;

namespace ChatApp.Data.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        public int RoleId { get; set; }           

        public Roles Role { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } 













    }
}
