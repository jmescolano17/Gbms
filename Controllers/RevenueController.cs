using Gbms.Models;
using Gbms.Server.Services;
using Gbms.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Gbms.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RevenueController : Controller
    {
        RevenueServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public RevenueController(RevenueServices xservices, IWebHostEnvironment webHostEnvironment, IHubContext<Hub> hubContext)
        {
            this.xservices = xservices;
            _webHostEnvironment = webHostEnvironment;
            _hub = hubContext;

        }


        //View Resident
        [HttpGet]
        //[Authorize(Policy = "Admin")]
        public async Task<List<revenue>> Revenue()
        {
            var ret = await xservices.Revenue();
            return ret;
        }

        //Today Total Revenue
        [HttpGet]
        //[Authorize]
        public async Task<int> TodayTotalRevenue()
        {
            return await xservices.TodayTotalRevenue();
        }

        //Month Total Revenue
        [HttpGet]
        //[Authorize]
        public async Task<int> MonthTotalRevenue()
        {
            return await xservices.MonthTotalRevenue();
        }

        //Count Transactions
        [HttpGet]
        //[Authorize]
        public async Task<int> Recent()
        {
            return await xservices.Recent();
        }

    }
}
