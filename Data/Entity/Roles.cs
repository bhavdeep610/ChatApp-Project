using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Data.Entity
{
    public class Roles
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
