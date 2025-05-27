using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SocialMediaAPI.Models
{
    public class Hashtag
    {
        public int Id { get; set; }
        public string Tag { get; set; } = string.Empty;
    }
}