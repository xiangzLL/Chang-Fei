using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces.Grains;
using Orleans;
using Orleans.Providers;

namespace ChangFei.Grains.Grains
{
    public class GroupState
    {
        /// <summary>
        /// Users are contained in group.
        /// </summary>
        public Dictionary<string,IMessageSubscriber> Users { get; set; }
    }

    [StorageProvider(ProviderName = "MessageStore")]
    public class GroupGrain: Grain<GroupState>,IGroupGrain
    {
        public string GroupId => this.GetPrimaryKeyString();

        #region Grain overrides

        public override Task OnActivateAsync()
        {
            if (State.Users == null)
            {
                State.Users = new Dictionary<string, IMessageSubscriber>();
            }
            return base.OnActivateAsync();
        }

        #endregion

        public async Task NewMessageAsync(Message message)
        {
            //publish message to other users, beside original user
            var users = State.Users.Where(user => user.Key != message.Sender).Select(user=>user.Value);
            await Task.WhenAll(users.Select(user => user.NewMessageAsync(message)));
        }

        public Task SubscribeAsync(string userId, IMessageSubscriber viewer)
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
