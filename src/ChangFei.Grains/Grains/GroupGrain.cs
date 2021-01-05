using System.Collections.Generic;
using System.Threading.Tasks;
using ChangFei.Interfaces.Grains;
using Orleans;

namespace ChangFei.Grains.Grains
{
    public class GroupGrain: Grain,IGroupGrain
    {
        private readonly List<string> _users = new List<string>();

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }

        public Task SendChatMessageAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        

    }
}
