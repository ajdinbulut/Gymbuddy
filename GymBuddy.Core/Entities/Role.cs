﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gymbuddy.Core.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
