using Gbms.Class;
using Gbms.Models;
using Gbms.Shared;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Server.Services
{
    public class IndigencyServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public IndigencyServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Residency
        public async Task<List<indigency>> Indigency()
        {

            List<indigency> xresidency = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewIndigency", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xresidency.Add(new indigency
                    {
                        indi_id = rdr["indi_id"].ToString(),
                        res_id = rdr["res_id"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        age = Convert.ToInt32(rdr["age"]),
                        citizenship = rdr["citizenship"].ToString(),
                        status = rdr["status"].ToString(),
                        purok = rdr["purok"].ToString(),
                        request = rdr["request"].ToString(),
                        date = Convert.ToDateTime( rdr["date"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        stat = rdr["stat"].ToString(),
                        clerk = rdr["clerk"].ToString()
                    });
                }
            }
            return xresidency;
        }

        //Insert Indigency
        public async Task<int> AddIndigency(indigency xresidency)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertIndigency", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_type", xresidency.type);
                com.Parameters.AddWithValue("_indi_id", xresidency.indi_id);
                com.Parameters.AddWithValue("_res_id", xresidency.res_id);
                com.Parameters.AddWithValue("_request", xresidency.request);
                com.Parameters.AddWithValue("_date", xresidency.date);
                com.Parameters.AddWithValue("_fee", xresidency.fee);
                com.Parameters.AddWithValue("_receipt", xresidency.receipt);
                com.Parameters.AddWithValue("_stat", xresidency.stat);
                com.Parameters.AddWithValue("_user_id", xresidency.clerk);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
            return i;
        }

        //Update Indigency
        public async Task<int> UpdateIndigency(indigency xresidency)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateIndigency", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_type", xresidency.type);
                com.Parameters.AddWithValue("_indi_id", xresidency.indi_id);
                com.Parameters.AddWithValue("_res_id", xresidency.res_id);
                com.Parameters.AddWithValue("_request", xresidency.request);
                com.Parameters.AddWithValue("_date", xresidency.date);
                com.Parameters.AddWithValue("_fee", xresidency.fee);
                com.Parameters.AddWithValue("_receipt", xresidency.receipt);
                com.Parameters.AddWithValue("_stat", xresidency.stat);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
            return i;
        }

        //Count Indigency
        public async Task<int> TotalIndigency()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM indigency WHERE DATE(date) = CURDATE();", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Search Date Indigency
        public async Task<List<indigency>> SearchDateIndigency(DateTime start, DateTime end)
        {
            List<indigency> xindigency = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("SearchDateIndigency", con)
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
                            xindigency.Add(new indigency
                            {
                                indi_id = rdr["indi_id"].ToString(),
                                res_id = rdr["res_id"].ToString(),
                                fullname = rdr["fullname"].ToString(),
                                age = Convert.ToInt32(rdr["age"]),
                                citizenship = rdr["citizenship"].ToString(),
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
            return xindigency;
        }

        //Indigency Report By Date
        public async Task<List<indigency>> IndigencyReport(DateTime start, DateTime end)
        {
            List<indigency> xindigency = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("ReportDateIndigency", con)
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
                            xindigency.Add(new indigency
                            {
                                indi_id = rdr["indi_id"].ToString(),
                                res_id = rdr["res_id"].ToString(),
                                fullname = rdr["fullname"].ToString(),
                                age = Convert.ToInt32(rdr["age"]),
                                citizenship = rdr["citizenship"].ToString(),
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
            return xindigency;
        }

        //Search Indigency
        public async Task<List<indigency>> SearchIndigency(string search)
        {

            List<indigency> xresidency = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchIndigency", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xresidency.Add(new indigency
                    {
                        indi_id = rdr["indi_id"].ToString(),
                        res_id = rdr["res_id"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        age = Convert.ToInt32(rdr["age"]),
                        citizenship = rdr["citizenship"].ToString(),
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
            return xresidency;
        }


    }

}

