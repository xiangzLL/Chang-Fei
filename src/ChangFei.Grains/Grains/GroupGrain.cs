using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using ChangFei.Core.Message;
using ChangFei.Interfaces.Grains;
using Orleans;

namespace ChangFei.Grains.Grains
{
    public class GroupGrain: Grain,IGroupGrain
    {
        private readonly List<string> _users = new List<string>();

        public Task<ImmutableList<Message>> GetOfflineMessages()
        {
            throw new System.NotImplementedException();
        }

        public Task NewMessageAsync(Message message)
        {
            throw new System.NotImplementedException();
        }

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public Task SendMessageAsync(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}
