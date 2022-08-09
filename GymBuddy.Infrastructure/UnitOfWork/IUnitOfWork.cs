using GymBuddy.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole { get; }
        IUserCountryRepository UserCountry { get; }
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        IPostCommentRepository PostComment { get; }
        void Save();
    }
}
