using Gymbuddy.Core.Entities;
using Gymbuddy.Infrastructure;
using GymBuddy.Core.Entities;
using GymBuddy.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.Repository
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly GymDB _db;
       public ChatRepository(GymDB db) : base(db)
        {
            _db = db;
        }
      
        public void Update(Chat obj)
        {
            _db.Chats.Update(obj);
        }
    }
}
