using Gbms.Class;
using Gbms.Models;
using Gbms.Shared;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace Gbms.Server.Services
{
    public class HouseholdServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;

        public HouseholdServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Household
        public async Task<List<household>> Household()
        {

            List<household> xhousehold = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewHousehold", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xhousehold.Add(new household
                    {
                        house_id = rdr["house_id"].ToString(),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        ext = rdr["ext"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        householdno = rdr["householdno"].ToString(),
                        gender = rdr["gender"].ToString(),
                        purok = rdr["purok"].ToString(),
                        male = Convert.ToInt32(rdr["male"]),
                        female = Convert.ToInt32(rdr["female"]),
                        total = Convert.ToInt32(rdr["total"]),
                        status = rdr["status"].ToString(),
                    });
                }
            }
            return xhousehold;
        }

        //Report Household
        public async Task<List<household>> HouseholdReport()
        {

            List<household> xhousehold = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ReportHousehold", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xhousehold.Add(new household
                    {
                        house_id = rdr["house_id"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        householdno = rdr["householdno"].ToString(),
                        gender = rdr["gender"].ToString(),
                        purok = rdr["purok"].ToString(),
                        male = Convert.ToInt32(rdr["male"]),
                        female = Convert.ToInt32(rdr["female"]),
                        total = Convert.ToInt32(rdr["total"]),
                        status = rdr["status"].ToString(),
                    });
                }
            }
            return xhousehold;
        }

        //Insert Household
        public async Task<int> AddHousehold(household xhousehold)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertHousehold", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_house_id", xhousehold.house_id);
                com.Parameters.AddWithValue("_fname", xhousehold.fname);
                com.Parameters.AddWithValue("_mname", xhousehold.mname);
                com.Parameters.AddWithValue("_lname", xhousehold.lname);
                com.Parameters.AddWithValue("_ext", xhousehold.ext);
                com.Parameters.AddWithValue("_householdno", xhousehold.householdno);
                com.Parameters.AddWithValue("_gender", xhousehold.gender);
                com.Parameters.AddWithValue("_purok", xhousehold.purok);
                com.Parameters.AddWithValue("_male", xhousehold.male);
                com.Parameters.AddWithValue("_female", xhousehold.female);
                com.Parameters.AddWithValue("_total", xhousehold.total);
                com.Parameters.AddWithValue("_status", xhousehold.status);

                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }

        //Update Household
        public async Task<int> UpdateHousehold(household xhousehold)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateHousehold", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_house_id", xhousehold.house_id);
                com.Parameters.AddWithValue("_fname", xhousehold.fname);
                com.Parameters.AddWithValue("_mname", xhousehold.mname);
                com.Parameters.AddWithValue("_lname", xhousehold.lname);
                com.Parameters.AddWithValue("_ext", xhousehold.ext);
                com.Parameters.AddWithValue("_householdno", xhousehold.householdno);
                com.Parameters.AddWithValue("_gender", xhousehold.gender);
                com.Parameters.AddWithValue("_purok", xhousehold.purok);
                com.Parameters.AddWithValue("_male", xhousehold.male);
                com.Parameters.AddWithValue("_female", xhousehold.female);
                com.Parameters.AddWithValue("_total", xhousehold.total);
                com.Parameters.AddWithValue("_status", xhousehold.status);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }


        //Search Household
        public async Task<List<household>> SearchHousehold(string search)
        {
            List<household> _household = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchHousehold", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    _household.Add(new household
                    {
                        house_id = rdr["house_id"].ToString(),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        ext = rdr["ext"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        householdno = rdr["householdno"].ToString(),
                        gender = rdr["gender"].ToString(),
                        purok = rdr["purok"].ToString(),
                        male = Convert.ToInt32(rdr["male"]),
                        female = Convert.ToInt32(rdr["female"]),
                        total = Convert.ToInt32(rdr["total"]),
                        status = rdr["status"].ToString(),
                    });
                }
            }
            return _household;
        }

        //Count Household
        public async Task<int> TotalHousehold()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM household", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

    }
}
