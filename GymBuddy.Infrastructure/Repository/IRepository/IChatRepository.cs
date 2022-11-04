﻿using Gymbuddy.Core.Entities;
using GymBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Infrastructure.Repository.IRepository
{
    public interface IChatRepository : IRepository<Chat>
    {
        void Update(Chat obj);

    }
}
