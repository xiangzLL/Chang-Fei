using System;
using System.Collections.Generic;

namespace ChangFei.Route.Model
{
    public class Group:BaseEntity
    {
        public string GroupName { get; set; }

        public string Notice { get; set; }

        public List<GroupUser> Users { get; set; }
    }

    public class GroupUser
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string UserNickName { get; set; }

        public DateTime JoinTime { get; set; }

        public DateTime ExitTime { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsOwner { get; set; }

        public bool IsManager { get; set; }
    }
}
