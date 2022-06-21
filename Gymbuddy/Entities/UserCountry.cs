using System.ComponentModel.DataAnnotations.Schema;

namespace Gymbuddy.Entities
{
    public class UserCountry
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
    }
}
