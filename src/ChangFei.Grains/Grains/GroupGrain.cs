using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces.Grains;
using Orleans;

namespace ChangFei.Grains.Grains
{
    public class GroupState
    {
        /// <summary>
        /// Users are contained in group.
        /// </summary>
        public Dictionary<string,IGroupMessageSubscriber> Users { get; set; }
    }

    public class GroupGrain: Grain<GroupState>,IGroupGrain
    {
        public string GroupId => this.GetPrimaryKeyString();

        #region Grain overrides

        public override Task OnActivateAsync()
        {
            if (State.Users == null)
            {
                State.Users = new Dictionary<string, IGroupMessageSubscriber>();
            }
            return base.OnActivateAsync();
        }

        #endregion

        public async Task NewMessageAsync(Message message)
        {
            //store this message record to db

            //publish message to other users, beside original user
            foreach (var user in State.Users.Where(user => user.Key != message.UserId))
            {
                await user.Value.NewGroupMessageAsync(Message.ConvertToResponseMessage(message));
            }
        }

        public Task SubscribeAsync(string userId, IGroupMessageSubscriber viewer)
        {
            State.Users[userId] = viewer;
            return Task.CompletedTask;
        }

        public Task UnsubscribeAsync(string userId)
        {
            State.Users.Remove(userId);
            return Task.CompletedTask;
        }
    }
}
