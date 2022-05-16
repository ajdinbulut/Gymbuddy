using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gymbuddy.Entities
{
    public class CompetingUser
    {   
        public int Id { get; set; }
        public string username { get; set; }
        public int bench { get; set; }
        public int deadlift { get; set; }
        public int squat { get; set; }
        public int total { get; set; }  
       [Required]
       [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual User Users { get; set; }
    }
}
