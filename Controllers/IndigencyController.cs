using Gbms.Class;
using Gbms.Models;
using Gbms.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Reporting.NETCore;
using System.Data;

namespace Gbms.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IndigencyController : Controller
    {
        IndigencyServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public IndigencyController(IndigencyServices xservices, IHubContext<Hub> hubcontext, IWebHostEnvironment webHostEnvironment)
        {
            this.xservices = xservices;
            _hub = hubcontext;
            _webHostEnvironment = webHostEnvironment;
        }

        //View Indigency
        [HttpGet]
        //[Authorize]
        public async Task<List<indigency>> Indigency()
        {
            var ret = await xservices.Indigency();
            return ret;
        }

        //Add Indigency
        [HttpPost]
        //[Authorize]
        public async Task<int> AddIndigency([FromBody] indigency xindigency)
        {
            var ret = await xservices.AddIndigency(xindigency);
            return ret;
        }

        //Update Indigency
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateIndigency([FromBody] indigency xindigency)
        {
            var ret = await xservices.UpdateIndigency(xindigency);
            return ret;
        }

        //Count Indigency
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalIndigency()
        {
            return await xservices.TotalIndigency();
        }

        //Search Date  Indigency
        [HttpGet]
        public async Task<List<indigency>> SearchDateIndigency(DateTime start, DateTime end)
        {
            var ret = await xservices.SearchDateIndigency(start, end);
            return ret;
        }

        //Indigency Report By Date
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IndigencyReport(DateTime start, DateTime end)
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.IndigencyReport(start, end);
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "IndigencyReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "Indigency.xls");
        }

        //Search Indigency
        [HttpGet]
        //[Authorize]
        public async Task<List<indigency>> SearchIndigency(string search)
        {
            var ret = await xservices.SearchIndigency(search);
            return ret;
        }
    }

}

