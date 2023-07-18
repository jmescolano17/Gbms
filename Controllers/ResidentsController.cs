using Gbms.Class;
using Gbms.Models;
using Gbms.Server.Services;
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

    public class ResidentsController : Controller
    {
        ResidentsServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public ResidentsController(ResidentsServices xservices, IWebHostEnvironment webHostEnvironment, IHubContext<Hub> hubContext)
        {
            this.xservices = xservices;
            _webHostEnvironment = webHostEnvironment;
            _hub = hubContext;

        }


        //View Resident
        [HttpGet]
        //[Authorize(Policy = "Admin")]
        public async Task<List<residents>> Residents()
        {
            var ret = await xservices.Residents();
            return ret;
        }

        //Insert Residents
        [HttpPost]
        //[Authorize(Policy = "Admin")]
        public async Task<int> AddResidents([FromBody] residents xresidents)
        {
            var ret = await xservices.AddResidents(xresidents);
            return ret;
        }

        //Update Residents
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateResidents([FromBody] residents xresidents)
        {
            var ret = await xservices.UpdateResidents(xresidents);
            return ret;
        }


        //Search Residents
        [HttpGet]
        public async Task<List<residents>> SearchResidents(string search)
        {
            var ret = await xservices.SearchResidents(search);
            return ret;
        }

        //Count Residents
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalResidents()
        {
            return await xservices.TotalResidents();
        }

        //Count Male
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalMale()
        {
            return await xservices.TotalMale();
        }

        //Count Female
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalFemale()
        {
            return await xservices.TotalFemale();
        }

        //Residents Report
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Reports()
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.ResidentReport();
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "ResidentReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "Residents.xls");
        }
    }
}
