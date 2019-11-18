using System;
using System.Collections.Generic;
using System.Text;

namespace DSS.Models
{
    public class User
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public int Age { get; set; }

        public string UserId { get; set; }
    }
}
