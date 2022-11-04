using Gymbuddy.Infrastructure;
using GymBuddy.Infrastructure.Repository;
using GymBuddy.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDB _db;
        public UnitOfWork(GymDB db)
        {
            _db = db;
            User = new UserRepository(_db);
            Role = new RoleRepository(_db);
            UserRole = new UserRoleRepository(_db);
            UserCountry = new UserCountryRepository(_db);
            Post = new PostRepository(_db);
            Comment = new CommentRepository(_db);
            PostLikes = new PostLikesRepository(_db);
            Follow = new FollowRepository(_db);
            Chat = new ChatRepository(_db);
            Connection = new ConnectionRepository(_db);
        }
        public IUserRepository User { get; private set; }

        public IRoleRepository Role { get; private set; }

        public IUserRoleRepository UserRole { get; private set; }
        public IUserCountryRepository UserCountry { get; private set; }
        public IPostRepository Post { get; private set; }
        public ICommentRepository Comment { get; private set; }
        public IPostLikesRepository PostLikes { get; private set; }
        public IFollowRepository Follow { get; private set; }
        public IChatRepository Chat { get; private set; }
        public IConnectionRepository Connection { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
