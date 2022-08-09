﻿using Gymbuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymBuddy.Core.Entities
{
    public class PostComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int? CommentId { get; set; }
        public Comment? Comment { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
