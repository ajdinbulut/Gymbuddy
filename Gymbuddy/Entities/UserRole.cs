using System.ComponentModel.DataAnnotations.Schema;

namespace Gymbuddy.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

  
    }
}
