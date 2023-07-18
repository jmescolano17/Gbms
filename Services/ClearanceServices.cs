using Gbms.Class;
using Gbms.Models;
using Gbms.Pages;
using Gbms.Shared;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Server.Services
{
    public class ClearanceServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;

        public ClearanceServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Clearance
        public async Task<List<clearance>> Clearance()
        {

            List<clearance> xclearance = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewClearance", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xclearance.Add(new clearance
                    {
                        clear_id = rdr["clear_id"].ToString(),
                        res_id = rdr["res_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        fullname = rdr["fullname"].ToString(),
                        age = Convert.ToInt32(rdr["age"].ToString()),
                        status = rdr["status"].ToString(),
                        purok = rdr["purok"].ToString(),
                        request = rdr["request"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        stat = rdr["stat"].ToString(),
                        clerk = rdr["clerk"].ToString()
                    });
                }
            }
            return xclearance;
        }

        //Insert Clearance
        public async Task<int> AddClearance(clearance xclearance)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertClearance", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_type", xclearance.type);
                com.Parameters.AddWithValue("_clear_id", xclearance.clear_id);
                com.Parameters.AddWithValue("_res_id", xclearance.res_id);
                com.Parameters.AddWithValue("_photo", xclearance.photo);
                com.Parameters.AddWithValue("_request", xclearance.request);
                com.Parameters.AddWithValue("_date", xclearance.date);
                com.Parameters.AddWithValue("_fee", xclearance.fee);
                com.Parameters.AddWithValue("_receipt", xclearance.receipt);
                com.Parameters.AddWithValue("_stat", xclearance.stat);
                com.Parameters.AddWithValue("_user_id", xclearance.clerk);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }

        //Update Clearance
        public async Task<int> UpdateClearance(clearance xclearance)
        {
            try
            {
                int i = 0;
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("UpdateClearance", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("_type", xclearance.type);
                    com.Parameters.AddWithValue("_clear_id", xclearance.clear_id);
                    com.Parameters.AddWithValue("_res_id", xclearance.res_id);
                    com.Parameters.AddWithValue("_photo", xclearance.photo);
                    com.Parameters.AddWithValue("_fname", xclearance.fname);
                    com.Parameters.AddWithValue("_mname", xclearance.mname);
                    com.Parameters.AddWithValue("_lname", xclearance.lname);
                    com.Parameters.AddWithValue("_ext", xclearance.ext);
                    com.Parameters.AddWithValue("_age", xclearance.age);
                    com.Parameters.AddWithValue("_status", xclearance.status);
                    com.Parameters.AddWithValue("_purok", xclearance.purok);
                    com.Parameters.AddWithValue("_request", xclearance.request);
                    com.Parameters.AddWithValue("_date", xclearance.date);
                    com.Parameters.AddWithValue("_fee", xclearance.fee);
                    com.Parameters.AddWithValue("_receipt", xclearance.receipt);
                    com.Parameters.AddWithValue("_stat", xclearance.stat);
                    i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
                return i;
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                return -1;
            }
        }

        //Count Clearance
        public async Task<int> TotalClearance()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM clearance WHERE DATE(date) = CURDATE();", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Search Date Clearance
        public async Task<List<clearance>> SearchDateClearance(DateTime start, DateTime end)
        {
            List<clearance> xclearance = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("SearchDateClearance", con)
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
                            xclearance.Add(new clearance
                            {
                                clear_id = rdr["clear_id"].ToString(),
                                res_id = rdr["res_id"].ToString(),
                                photo = (byte[])rdr["photo"],
                                fullname = rdr["fullname"].ToString(),
                                age = Convert.ToInt32(rdr["age"].ToString()),
                                status = rdr["status"].ToString(),
                                purok = rdr["purok"].ToString(),
                                request = rdr["request"].ToString(),
                                date = Convert.ToDateTime(rdr["date"]),
                                fee = rdr["fee"].ToString(),
                                receipt = rdr["receipt"].ToString(),
                                stat = rdr["stat"].ToString(),
                                clerk = rdr["clerk"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xclearance;
        }

        //Clearance Report By Date
        public async Task<List<clearance>> ClearanceReport(DateTime start, DateTime end)
        {
            List<clearance> xclearance = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("ReportDateClearance", con)
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
                            xclearance.Add(new clearance
                            {
                                clear_id = rdr["clear_id"].ToString(),
                                res_id = rdr["res_id"].ToString(),
                                photo = (byte[])rdr["photo"],
                                fullname = rdr["fullname"].ToString(),
                                age = Convert.ToInt32(rdr["age"].ToString()),
                                status = rdr["status"].ToString(),
                                purok = rdr["purok"].ToString(),
                                request = rdr["request"].ToString(),
                                date = Convert.ToDateTime(rdr["date"]),
                                fee = rdr["fee"].ToString(),
                                receipt = rdr["receipt"].ToString(),
                                stat = rdr["stat"].ToString(),
                                clerk = rdr["clerk"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xclearance;
        }

        //Search Clearance
        public async Task<List<clearance>> SearchClearance(string search)
        {
            List<clearance> xclearance = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchClearance", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xclearance.Add(new clearance
                    {
                        clear_id = rdr["clear_id"].ToString(),
                        res_id = rdr["res_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        fullname = rdr["fullname"].ToString(),
                        age = Convert.ToInt32(rdr["age"].ToString()),
                        status = rdr["status"].ToString(),
                        purok = rdr["purok"].ToString(),
                        request = rdr["request"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        stat = rdr["stat"].ToString(),
                        clerk = rdr["clerk"].ToString()
                    });
                }
            }
            return xclearance;
        }

    }
}
