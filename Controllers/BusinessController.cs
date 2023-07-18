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
    public class BusinessController : Controller
    {
        BusinessServices xservices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        IHubContext<Hub> _hub;

        public BusinessController(BusinessServices xservices, IHubContext<Hub> hubContext, IWebHostEnvironment webHostEnvironment)
        {
            this.xservices = xservices;
            _hub = hubContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //View Business Permit
        [HttpGet]
        //[Authorize]
        public async Task<List<business>> Business()
        {
            var ret = await xservices.Business();
            return ret;
        }

        //Add Business Permit
        [HttpPost]
        //[Authorize]
        public async Task<int> AddBusiness([FromBody] business xbusiness)
        {
            var ret = await xservices.AddBusiness(xbusiness);
            return ret;
        }

        //Update Business Permit
        [HttpPost]
        //[Authorize]
        public async Task<int> UpdateBusiness([FromBody] business xbusiness)
        {
            var ret = await xservices.UpdateBusiness(xbusiness);
            return ret;
        }

        //Count Business Permit
        [HttpGet]
        //[Authorize]
        public async Task<int> TotalBusiness()
        {
            return await xservices.TotalBusiness();
        }

        //Search Date  Business
        [HttpGet]
        public async Task<List<business>> SearchDateBusiness(DateTime start, DateTime end)
        {
            var ret = await xservices.SearchDateBusiness(start, end);
            return ret;
        }

        //Business Report By Date
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BusinessReport(DateTime start, DateTime end)
        {
            ListtoTable listtoTable = new();
            var dt = new DataTable();
            var lst = await xservices.BusinessReport(start, end);
            dt = listtoTable.ToDataTablee(lst);
            string reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Reports", "BusinessReport.rdlc");
            Stream reportDefinition;

            using var fs = new FileStream(reportPath, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt));
            byte[] excel = report.Render("EXCEL");
            fs.Dispose();

            return File(excel, "application/msexcel", "Business.xls");
        }

        //Search Business Permit
        [HttpGet]
        //[Authorize]
        public async Task<List<business>> SearchBusiness(string search)
        {
            var ret = await xservices.SearchBusiness(search);
            return ret;
        }
    }
}
