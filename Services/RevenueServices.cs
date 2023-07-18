using Gbms.Class;
using Gbms.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Services
{
    public class RevenueServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;

        public RevenueServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Residents
        public async Task<List<revenue>> Revenue()
        {

            List<revenue> xrevenue = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("Revenue", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xrevenue.Add(new revenue
                    {
                        date = Convert.ToDateTime(rdr["date"]),
                        fullname = rdr["fullname"].ToString(),
                        type = rdr["type"].ToString(),
                        fee = rdr["fee"].ToString()
                    });
                }
            }
            return xrevenue;
        }

        //Total Revenue Today
        public async Task<int> TodayTotalRevenue()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("TodayTotalRevenue", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Total Revenue By Month
        public async Task<int> MonthTotalRevenue()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("MonthlyTotalRevenue", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        public async Task<int> Recent()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("Recent", con)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

    }
}
