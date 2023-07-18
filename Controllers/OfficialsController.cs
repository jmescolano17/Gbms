using Gbms.Models;
using Gbms.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Gbms.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficialsController : Controller
    {
        OfficialsServices xservices;
        IHubContext<Hub> _hub;
        public OfficialsController(OfficialsServices xservices, IHubContext<Hub> hubcontext)
        {
            this.xservices = xservices;
            _hub = hubcontext;

        }

        //View Officials
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<List<officials>> Officials()
        {
            var ret = await xservices.Officials();
            return ret;
        }


        //Insert Officials
        [HttpPost]
        //[Authorize]
        public async Task<int> AddOfficials([FromBody] officials xofficials)
        {
            var ret = await xservices.AddOfficials(xofficials);
            return ret;
        }

        //Update Officials
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateOfficials([FromBody] officials xofficials)
        {
            var ret = await xservices.UpdateOfficials(xofficials);
            return ret;
        }

        //Delete Officials
        [HttpPost]
        //[Authorize]
        public async Task<int> DeleteOfficials([FromBody] officials xofficials)
        {
            var ret = await xservices.DeleteOfficials(xofficials);
            return ret;
        }
        //Search Officials
        [HttpGet]
        public async Task<List<officials>> SearchOfficials(string search)
        {
            var ret = await xservices.SearchOfficials(search);
            return ret;
        }

    }
}
