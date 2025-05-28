using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int FollowerId { get; set; }

        public ICollection<UserFollow>? Followers { get; set; }
        public ICollection<UserFollow>? Followings { get; set; }

    }
}