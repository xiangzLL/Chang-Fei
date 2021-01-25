using System;
using System.Collections.Generic;

namespace ChangFei.Gateway.Model
{
    public class Group
    {
        public string Id { get; set; }

        public string GroupName { get; set; }

        public string Notice { get; set; }

        public List<GroupUser> Users { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public class GroupUser
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public string UserNickName { get; set; }

        public DateTime JoinTime { get; set; }

        public DateTime ExitTime { get; set; }

        public bool IsExit { get; set; }
    }
}
