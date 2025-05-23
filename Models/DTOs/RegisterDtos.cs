using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.DTOs
{
    public class RegisterDtos
    {

        [Required]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

        public int RoleId { get; set; } = 1; // Default role ID

    }
}
