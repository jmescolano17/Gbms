using Gbms.Class;
using Gbms.Models;
using Gbms.Server.Services;
using Gbms.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Reporting.NETCore;
using System.Data;

namespace Gbms.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlotterController : Controller
    {
        BlotterServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public BlotterController(BlotterServices xservices, IHubContext<Hub> hubContext, IWebHostEnvironment webHostEnvironment)
        {
            this.xservices = xservices;
            _hub = hubContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //View Blotter
        [HttpGet]
        //[Authorize(Policy = "Admin")]
        public async Task<List<blotter>> Blotter()
        {
            var ret = await xservices.Blotter();
            return ret;
        }


        //Add Blotter
        [HttpPost]
        //[Authorize]
        public async Task<int> AddBlotter([FromBody] blotter xblotter)
        {
            var ret = await xservices.AddBlotter(xblotter);
            return ret;
        }

        //Update Blotter
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateBlotter([FromBody] blotter xblotter)
        {
            var ret = await xservices.UpdateBlotter(xblotter);
            return ret;
        }

        //Search Blotter
        [HttpGet]
        //[Authorize]
        public async Task<List<blotter>> SearchBlotter(string search)
        {
            var ret = await xservices.SearchBlotter(search);
            return ret;
        }

        //Count Blotter Pending
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalPending()
        {
            return await xservices.TotalPending();
        }

        //Count Blotter Resolved
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalResolved()
        {
            return await xservices.TotalResolved();
        }

        //Search Date  Blotter
        [HttpGet]
        //[Authorize]
        public async Task<List<blotter>> SearchDateBlotter(DateTime start, DateTime end)
        {
            var ret = await xservices.SearchDateBlotter(start, end);
            return ret;
        }


        //Blotter Report By Date
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BlotterReport(DateTime start, DateTime end)
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.SearchDateBlotter(start, end);
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "BlotterReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "Blotter.xls");
        }

    }
}
