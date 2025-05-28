using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Models
{
    public class UserFollow
    {
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }

        public User? Follower { get; set; }
        public User? Following { get; set; }
    }
}