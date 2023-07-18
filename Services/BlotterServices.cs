using Gbms.Class;
using Gbms.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Services
{
    public class BlotterServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public BlotterServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Blotter
        public async Task<List<blotter>> Blotter()
        {

            List<blotter> xblotter = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewBlotter", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xblotter.Add(new blotter
                    {
                        blotter_id = rdr["blotter_id"].ToString(),
                        case_no = rdr["case_no"].ToString(),
                        res_id_c = rdr["res_id_c"].ToString(),
                        complainant = rdr["complainant"].ToString(),
                        res_id_r = rdr["res_id_r"].ToString(),
                        respondent = rdr["respondent"].ToString(),
                        title = rdr["title"].ToString(),
                        details = rdr["details"].ToString(),
                        rdate = Convert.ToDateTime(rdr["rdate"]),
                        sched = Convert.ToDateTime(rdr["sched"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        status = rdr["status"].ToString(),
                    });
                }
            }
            return xblotter;
        }

        //Insert Blotter
        public async Task<int> AddBlotter(blotter xblotter)
        {
            int i = 0;
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("InsertBlotter", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("_type", xblotter.type);
                    com.Parameters.AddWithValue("_blotter_id", xblotter.blotter_id);
                    com.Parameters.AddWithValue("_case_no", xblotter.case_no);
                    com.Parameters.AddWithValue("_res_id_c", xblotter.res_id_c);
                    com.Parameters.AddWithValue("_res_id_r", xblotter.res_id_r);
                    com.Parameters.AddWithValue("_title", xblotter.title);
                    com.Parameters.AddWithValue("_details", xblotter.details);
                    com.Parameters.AddWithValue("_rdate", xblotter.rdate);
                    com.Parameters.AddWithValue("_sched", xblotter.sched);
                    com.Parameters.AddWithValue("_fee", xblotter.fee);
                    com.Parameters.AddWithValue("_receipt", xblotter.receipt);
                    com.Parameters.AddWithValue("_status", xblotter.status);
                    i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine(ex.Message);
            }
            return i;
        }


        //Update Blotter
        public async Task<int> UpdateBlotter(blotter xblotter)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateBlotter", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_type", xblotter.type);
                com.Parameters.AddWithValue("_blotter_id", xblotter.blotter_id);
                com.Parameters.AddWithValue("_case_no", xblotter.case_no);
                com.Parameters.AddWithValue("_res_id_c", xblotter.res_id_c);
                com.Parameters.AddWithValue("_res_id_r", xblotter.res_id_r);
                com.Parameters.AddWithValue("_title", xblotter.title);
                com.Parameters.AddWithValue("_details", xblotter.details);
                com.Parameters.AddWithValue("_rdate", xblotter.rdate);
                com.Parameters.AddWithValue("_sched", xblotter.sched);
                com.Parameters.AddWithValue("_fee", xblotter.fee);
                com.Parameters.AddWithValue("_receipt", xblotter.receipt);
                com.Parameters.AddWithValue("_status", xblotter.status);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }

        //Search Blotter
        public async Task<List<blotter>> SearchBlotter(string search)
        {

            List<blotter> xblotter = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchBlotter", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xblotter.Add(new blotter
                    {
                        blotter_id = rdr["blotter_id"].ToString(),
                        case_no = rdr["case_no"].ToString(),
                        complainant = rdr["complainant"].ToString(),
                        respondent = rdr["respondent"].ToString(),
                        title = rdr["title"].ToString(),
                        details = rdr["details"].ToString(),
                        sched = Convert.ToDateTime(rdr["sched"]),
                        rdate = Convert.ToDateTime(rdr["rdate"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        status = rdr["status"].ToString(),
                    });
                }
            }
            return xblotter;
        }


        //Count Blotter Pending
        public async Task<int> TotalPending()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM blotter WHERE status = 'Pending' ", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Count Blotter Resolved
        public async Task<int> TotalResolved()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM blotter WHERE status = 'Resolved' ", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }


        //Search Date Blotter
        public async Task<List<blotter>> SearchDateBlotter(DateTime start, DateTime end)
        {
            List<blotter> xblotter = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("SearchDateBlotter", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("start_date", start);
                    com.Parameters.AddWithValue("end_date", end);

                    using (var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await rdr.ReadAsync().ConfigureAwait(false))
                        {
                            xblotter.Add(new blotter
                            {
                                blotter_id = rdr["blotter_id"].ToString(),
                                case_no = rdr["case_no"].ToString(),
                                res_id_c = rdr["res_id_c"].ToString(),
                                complainant = rdr["complainant"].ToString(),
                                res_id_r = rdr["res_id_r"].ToString(),
                                respondent = rdr["respondent"].ToString(),
                                title = rdr["title"].ToString(),
                                details = rdr["details"].ToString(),
                                rdate = Convert.ToDateTime(rdr["rdate"]),
                                sched = Convert.ToDateTime(rdr["sched"]),
                                fee = rdr["fee"].ToString(),
                                receipt = rdr["receipt"].ToString(),
                                status = rdr["status"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xblotter;
        }


    }
}
