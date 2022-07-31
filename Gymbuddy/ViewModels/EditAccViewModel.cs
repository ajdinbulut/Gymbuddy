using Gymbuddy.Core.Entities;

namespace Gymbuddy.ViewModels
{
    public class EditAccViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public int Age { get; set; }
        public int RoleID { get; set; }

    }
}
