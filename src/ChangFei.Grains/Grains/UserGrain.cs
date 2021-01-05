using System;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces.Grains;
using Orleans;

namespace ChangFei.Grains.Grains
{
    public class UserGrain: Grain,IUserGrain
    {
        public bool IsLogined { get; private set; }

        public Task SendMessageAsync(Message message)
        {
            if (message.IsGroup)
            {
                var groupGrain = GrainFactory.GetGrain<IGroupGrain>(message.TargetId);
                return Task.CompletedTask;
            }

            var targetGrain = GrainFactory.GetGrain<IUserGrain>(message.TargetId);
            //targetGrain.
        }

        public Task Login()
        {
            IsLogined = true;
            return Task.CompletedTask;
        }

        public Task GetOfflineMessages()
        {
            throw new NotImplementedException();
        }
    }
}
