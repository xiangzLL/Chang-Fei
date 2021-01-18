using System;

namespace ChangFei.Grains.Entity
{
    /// <summary>
    /// Message record
    /// </summary>
    public class MessageRecord:BaseEntity
    {
        /// <summary>
        /// Sender user
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// Recipient user
        /// </summary>
        public User Recipient { get; set; }

        public BaseMessage Message { get; set; }

        public DateTime SendTime { get; set; }

        public MessageRecord(string id) : base(id)
        {
        }
    }

    public class User
    {
        public string UserId { get; set; }

        public string UserName { get; set; }
    }

}
