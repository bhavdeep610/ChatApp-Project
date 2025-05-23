using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Data.Entity
{
    public class Roles
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
