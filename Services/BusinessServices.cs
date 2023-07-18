using Gbms.Class;
using Gbms.Models;
using Gbms.Shared;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Server.Services
{
    public class BusinessServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public BusinessServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Business Permit
        public async Task<List<business>> Business()
        {

            List<business> xbusiness = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewBusiness", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xbusiness.Add(new business
                    {
                        bsns_id = rdr["bsns_id"].ToString(),
                        res_id = rdr["res_id"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        bsns_name = rdr["bsns_name"].ToString(),
                        purok = rdr["purok"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        stat = rdr["stat"].ToString(),
                        clerk = rdr["clerk"].ToString(),
                    });
                }
            }
            return xbusiness;
        }

        //Insert Business Permit
        public async Task<int> AddBusiness(business xbusiness)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertBusiness", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_type", xbusiness.type);
                com.Parameters.AddWithValue("_bsns_id", xbusiness.bsns_id);
                com.Parameters.AddWithValue("_res_id", xbusiness.res_id);
                com.Parameters.AddWithValue("_bsns_name", xbusiness.bsns_name);
                com.Parameters.AddWithValue("_purok", xbusiness.purok);
                com.Parameters.AddWithValue("_date", xbusiness.date);
                com.Parameters.AddWithValue("_fee", xbusiness.fee);
                com.Parameters.AddWithValue("_receipt", xbusiness.receipt);
                com.Parameters.AddWithValue("_stat", xbusiness.stat);
                com.Parameters.AddWithValue("_user_id", xbusiness.clerk);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }

        //Update Business Permit
        public async Task<int> UpdateBusiness(business xbusiness)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateBusiness", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_type", xbusiness.type);
                com.Parameters.AddWithValue("_bsns_id", xbusiness.bsns_id);
                com.Parameters.AddWithValue("_res_id", xbusiness.res_id);
                com.Parameters.AddWithValue("_bsns_name", xbusiness.bsns_name);
                com.Parameters.AddWithValue("_purok", xbusiness.purok);
                com.Parameters.AddWithValue("_date", xbusiness.date);
                com.Parameters.AddWithValue("_fee", xbusiness.fee);
                com.Parameters.AddWithValue("_receipt", xbusiness.receipt);
                com.Parameters.AddWithValue("_stat", xbusiness.stat);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }

        //Count Business Permit
        public async Task<int> TotalBusiness()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM business WHERE DATE(date) = CURDATE();", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Search Date Business
        public async Task<List<business>> SearchDateBusiness(DateTime start, DateTime end)
        {
            List<business> xbusiness = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("SearchDateBusiness", con)
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
                            xbusiness.Add(new business
                            {
                                bsns_id = rdr["bsns_id"].ToString(),
                                res_id = rdr["res_id"].ToString(),
                                fullname = rdr["fullname"].ToString(),
                                bsns_name = rdr["bsns_name"].ToString(),
                                purok = rdr["purok"].ToString(),
                                date = Convert.ToDateTime(rdr["date"]),
                                fee = rdr["fee"].ToString(),
                                receipt = rdr["receipt"].ToString(),
                                stat = rdr["stat"].ToString(),
                                clerk = rdr["clerk"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xbusiness;
        }

        // Business Report By Date
        public async Task<List<business>> BusinessReport(DateTime start, DateTime end)
        {
            List<business> xbusiness = new();
            try
            {
                using (var con = new MySqlConnection(_constring.GetConnection()))
                {
                    await con.OpenAsync().ConfigureAwait(false);
                    var com = new MySqlCommand("ReportDateBusiness", con)
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
                            xbusiness.Add(new business
                            {
                                bsns_id = rdr["bsns_id"].ToString(),
                                res_id = rdr["res_id"].ToString(),
                                fullname = rdr["fullname"].ToString(),
                                bsns_name = rdr["bsns_name"].ToString(),
                                purok = rdr["purok"].ToString(),
                                date = Convert.ToDateTime(rdr["date"]),
                                fee = rdr["fee"].ToString(),
                                receipt = rdr["receipt"].ToString(),
                                stat = rdr["stat"].ToString(),
                                clerk = rdr["clerk"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return xbusiness;
        }


        //Search Business Permit
        public async Task<List<business>> SearchBusiness(string search)
        {

            List<business> xbusiness = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchBusiness", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xbusiness.Add(new business
                    {
                        bsns_id = rdr["bsns_id"].ToString(),
                        res_id = rdr["res_id"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        bsns_name = rdr["bsns_name"].ToString(),
                        purok = rdr["purok"].ToString(),
                        date = Convert.ToDateTime(rdr["date"]),
                        fee = rdr["fee"].ToString(),
                        receipt = rdr["receipt"].ToString(),
                        stat = rdr["stat"].ToString(),
                        clerk = rdr["clerk"].ToString(),
                    });
                }
            }
            return xbusiness;
        }

    }

}

