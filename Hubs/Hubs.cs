using Microsoft.AspNetCore.SignalR;

namespace Gbms.Hubs
{
    public class Hubs :Hub
    {
        public async Task SendClient()
        {
            await Clients.All.SendAsync("client");
        }
    }
}
