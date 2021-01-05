using System;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces;
using ChangFei.Interfaces.Grains;
using Orleans;

namespace ChangFei.Grains.Grains
{
    public class UserGrain: Grain,IUserGrain
    {
        private IMessageViewer _messageViewer;

        public bool IsLogined { get; private set; }

        public string UserId => this.GetPrimaryKeyString();

        public Task SendMessageAsync(Message message)
        {
            var targetGrain = GrainFactory.GetGrain<IUserGrain>(message.TargetId);
            return targetGrain.ReceiveMessageAsync(message);
        }

        public Task ReceiveMessageAsync(Message message)
        {
            if (_messageViewer != null)
            {
                _messageViewer.ReceiveUserMessage(Message.ConvertToResponseMessage(message));
            }
            else
            {
                //if user is not offline, Store record to db.

            }

            return Task.CompletedTask;
        }

        public Task Login(IMessageViewer viewer)
        {
            IsLogined = true;
            _messageViewer = viewer;
            return Task.CompletedTask;
        }

        public Task GetOfflineMessages()
        {
            throw new NotImplementedException();
        }
    }
}
