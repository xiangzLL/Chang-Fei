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
        /// User login state
        /// </summary>
        public bool Logined { get; set; }

        /// <summary>
        /// Message observer
        /// </summary>
        public IMessageViewer Viewer { get; set; }

        /// <summary>
        /// UnRead messages
        /// </summary>
        public Queue<Message> UnReadMessages { get; set; }

        /// <summary>
        /// UnRead group messages
        /// </summary>
        public Queue<Message> UnReadGroupMessages { get; set; }
    }

    [StorageProvider(ProviderName = "MessageStore")]
    public class UserGrain: Grain<UserState>, IUserGrain
    {
        public string UserId => this.GetPrimaryKeyString();

        #region Grain overrides

        public override Task OnActivateAsync()
        {
            if (State.UnReadMessages == null)
            {
                State.UnReadMessages = new Queue<Message>();
            }

            if (State.UnReadGroupMessages == null)
            {
                State.UnReadGroupMessages = new Queue<Message>();
            }
            return base.OnActivateAsync();
        }

        #endregion

        public Task SendMessageAsync(Message message)
        {
            //send message to target user
            var targetGrain = GrainFactory.GetGrain<IUserGrain>(message.TargetId);
            return targetGrain.NewMessageAsync(message);
        }

        public Task<ImmutableList<Message>> GetOfflineMessages()
        {
            return Task.FromResult(State.UnReadMessages.ToImmutableList());
        }

        public async Task NewMessageAsync(Message message)
        {
            if (State.Logined)
            {
                //if user is online, call message observer.
                State.Viewer.ReceiveUserMessage(Message.ConvertToResponseMessage(message));
                //store transfer message to db
            }
            else
            {
                //if user is not offline, Store message to queue.
                State.UnReadMessages.Enqueue(message);
                await WriteStateAsync();
            }
        }

        /// <summary>
        /// Receive a new group message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        public async Task NewGroupMessageAsync(Message message)
        {
            if (State.Logined)
            {
                //if user is online, call message observer.
                State.Viewer.ReceiveGroupMessage(message);
            }
            else
            {
                //if user is not offline, Store message to queue.
                State.UnReadGroupMessages.Enqueue(message);
                await WriteStateAsync();
            }
        }

        public async Task LoginAsync(IMessageViewer viewer)
        {
            State.Logined = true;
            State.Viewer = viewer;
            await WriteStateAsync();
            Console.WriteLine($"{UserId} login success.");
        }

        public async Task LogoutAsync()
        {
            State.Logined = false;
            State.Viewer = null;
            await WriteStateAsync();
            Console.WriteLine($"{UserId} logout success");
        }

        public Task SendGroupMessageAsync(Message message)
        {
            //send message to target group
            var targetGrain = GrainFactory.GetGrain<IGroupGrain>(message.TargetId);
            return targetGrain.NewMessageAsync(message);
        }

        public async Task SubscribeAsync(List<string> groupIds)
        {
            var groupGrains = new List<IGroupGrain>();
            groupIds.ForEach(groupId=>groupGrains.Add(GrainFactory.GetGrain<IGroupGrain>(groupId)));
            await Task.WhenAll(groupGrains.Select(_ => _.SubscribeAsync(UserId,this.AsReference<IGroupMessageSubscriber>())));
        }

        public Task UnsubscribeAsync(List<string> groupIds)
        {
            throw new NotImplementedException();
        }
    }
}
