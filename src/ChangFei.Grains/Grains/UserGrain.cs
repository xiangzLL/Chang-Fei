using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces;
using ChangFei.Interfaces.Grains;
using Orleans;
using Orleans.Providers;

namespace ChangFei.Grains.Grains
{
    /// <summary>
    /// User state
    /// </summary>
    [Serializable]
    public class UserState
    {
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User login state
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>
        /// Message observer
        /// </summary>
        public IMessageViewer Viewer { get; set; }

        /// <summary>
        /// UnRead message count
        /// </summary>
        public int UnReadMessagesCount => UnReadMessages.Count;

        /// <summary>
        /// UnRead messages
        /// </summary>
        public Queue<Message> UnReadMessages { get; set; }
    }

    [StorageProvider(ProviderName = "MessageStore")]
    public class UserGrain: Grain<UserState>, IUserGrain
    {
        public string UserId => this.GetPrimaryKeyString();

        #region Grain overrides

        public override Task OnActivateAsync()
        {
            State.UserId = this.GetPrimaryKeyString();

            if (State.UnReadMessages == null)
            {
                State.UnReadMessages = new Queue<Message>();
            }

            return base.OnActivateAsync();
        }

        #endregion

        #region User Behaviors

        public Task SendMessageAsync(Message message)
        {
            if (message.IsGroup) //send message to target group
            {
                var groupGrain = GrainFactory.GetGrain<IGroupGrain>(message.Recipient);
                return groupGrain.NewMessageAsync(message);
            }

            var targetGrain = GrainFactory.GetGrain<IUserGrain>(message.Recipient);
            return targetGrain.NewMessageAsync(message);
        }

        public Task<ImmutableList<Message>> GetUnReadMessages(int limit)
        {
            return Task.FromResult(State.UnReadMessages.ToImmutableList());
        }

        public async Task LoginAsync(IMessageViewer viewer)
        {
            State.IsLogin = true;
            State.Viewer = viewer;
            Console.WriteLine($"{UserId} login success, start dispatch unread messages.");
            while (State.UnReadMessages.Count > 0)
            {
                viewer.NewMessageAsync(State.UnReadMessages.Dequeue());
            }
            await WriteStateAsync();
            Console.WriteLine($"{UserId} login success.");
        }

        public async Task LogoutAsync()
        {
            State.IsLogin = false;
            State.Viewer = null;
            await WriteStateAsync();
            Console.WriteLine($"{UserId} logout success");
        }

        public async Task SubscribeAsync(List<string> groupIds)
        {
            var groupGrains = new List<IGroupGrain>();
            groupIds.ForEach(groupId => groupGrains.Add(GrainFactory.GetGrain<IGroupGrain>(groupId)));
            await Task.WhenAll(groupGrains.Select(_ => _.SubscribeAsync(UserId, this.AsReference<IMessageSubscriber>())));
        }

        public Task SubscribeAsync(string groupId)
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync(List<string> groupIds)
        {
            throw new NotImplementedException();
        }

        public Task UnSubscribeAsync(string userId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region New Message

        public async Task NewMessageAsync(Message message)
        {
            if (State.IsLogin)
            {
                //if user is online, call message observer.
                State.Viewer.NewMessageAsync(message);
                //store message to db
                var targetGrain = GrainFactory.GetGrain<IMessageStoreGrain>(0);
                await targetGrain.StoreMessageAsync(message);
            }
            else
            {
                //if user is not login, Store message to queue.
                State.UnReadMessages.Enqueue(message);
                await WriteStateAsync();
            }
        }

        #endregion

    }
}
