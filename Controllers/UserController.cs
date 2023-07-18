using Gbms.Models;
using Gbms.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gbms.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        UserServices xservices;

        public UserController(UserServices xservices)
        {
            this.xservices = xservices;

        }
        //Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<user>> Login(string user, string pwd)
        {
            var ret = await xservices.Login(user, pwd);
            return ret;
        }

        //Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<residents>> ResLogin(string resid)
        {
            var ret = await xservices.ResLogin(resid);
            return ret;
        }

        //View
        [HttpGet]
        //[Authorize]
        public new async Task<List<user>> User()
        {
            var ret = await xservices.User();
            return ret;
        }
    }
}
