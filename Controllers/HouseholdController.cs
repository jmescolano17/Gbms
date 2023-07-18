using Gbms.Server.Services;
using Gbms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Gbms.Class;
using Microsoft.Reporting.NETCore;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Gbms.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HouseHoldController : Controller
    {
        HouseholdServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public HouseHoldController(HouseholdServices xservices,IHubContext<Hub> _hubcontext, IWebHostEnvironment webHostEnvironment)
        {
            this.xservices = xservices;
            _hub = _hubcontext;
            _webHostEnvironment = webHostEnvironment;
        }
        //View Household
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<List<household>> Household()
        {
            var ret = await xservices.Household();
            return ret;
        }

        //Insert Household
        [HttpPost]
        //[Authorize]
        public async Task<int> AddHousehold([FromBody] household xhousehold)
        {
            var ret = await xservices.AddHousehold(xhousehold);
            return ret;
        }


        //Update Household
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateHousehold([FromBody] household xhousehold)
        {
            var ret = await xservices.UpdateHousehold(xhousehold);
            return ret;
        }


        //Search Household
        [HttpGet]
        public async Task<List<household>> SearchHousehold(string search)
        {
            var ret = await xservices.SearchHousehold(search);
            return ret;
        }

        //Count Household
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalHousehold()
        {
            return await xservices.TotalHousehold();
        }

        //Household Report
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> HouseholdReport()
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.HouseholdReport();
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "HouseholdReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "Household.xls");
        }
    }
}
