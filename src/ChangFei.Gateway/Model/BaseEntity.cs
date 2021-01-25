using System;
using ChangFei.Core.Utilities;

namespace ChangFei.Route.Model
{
    public class BaseEntity
    {
        public string Id { get; }

        public DateTime CreateTime { get;}

        public BaseEntity()
        {
            Id = IdHelper.Generate<string>();
            CreateTime = DateTime.Now;
        }
    }
}
