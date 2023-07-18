using Gbms.Server.Services;
using Gbms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Gbms.Class;
using Microsoft.Reporting.NETCore;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace Gbms.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClearanceController : ControllerBase
    {
        ClearanceServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public ClearanceController(ClearanceServices xservices, IHubContext<Hub> hubContext, IWebHostEnvironment webHostEnvironment)
        {
            this.xservices = xservices;
            _hub = hubContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //View Clearance
        [HttpGet]
        //[Authorize]
        public async Task<List<clearance>> Clearance()
        {
            var ret = await xservices.Clearance();
            return ret;
        }

        //Add Clearance
        [HttpPost]
        //[Authorize]
        public async Task<int> AddClearance([FromBody] clearance xclearance)
        {
            var ret = await xservices.AddClearance(xclearance);
            return ret;
        }

        //Update Clearance
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateClearance([FromBody] clearance xclearance)
        {
            var ret = await xservices.UpdateClearance(xclearance);
            return ret;
        }


        //Count Clearance
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalClearance()
        {
            return await xservices.TotalClearance();
        }

        //Search Date Clearance

        [HttpGet]
        public async Task<List<clearance>> SearchDateClearance(DateTime start, DateTime end)
        {
            var ret = await xservices.SearchDateClearance(start, end);
            return ret;
        }

        //Clearance Report By Date
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ClearanceReport(DateTime start, DateTime end)
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.ClearanceReport(start, end);
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "ClearanceReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "Clearance.xls");
        }

        //Search Clearance
        [HttpGet]
        public async Task<List<clearance>> SearchClearance(string search)
        {
            var ret = await xservices.SearchClearance(search);
            return ret;
        }
    }
}
