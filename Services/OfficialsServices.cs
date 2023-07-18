using Gbms.Class;
using Gbms.Models;
using Gbms.Shared;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Server.Services
{
    public class OfficialsServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public OfficialsServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Officials
        public async Task<List<officials>> Officials()
        {

            List<officials> officials = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewOfficials", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    officials.Add(new officials
                    {

                        off_id = rdr["off_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        position = rdr["position"].ToString(),
                        chairmanship = rdr["chairmanship"].ToString(),
                        contact = rdr["contact"].ToString(),
                        purok = rdr["purok"].ToString(),
                        term_start = Convert.ToDateTime(rdr["term_start"]).Date,
                        term_end = Convert.ToDateTime(rdr["term_end"]).Date,
                    });
                }
            }
            return officials;
        }

        //Insert Officials
        public async Task<int> AddOfficials(officials xofficials)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertOfficials", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_off_id", xofficials.off_id);
                com.Parameters.AddWithValue("_photo", xofficials.photo);
                com.Parameters.AddWithValue("_fname", xofficials.fname);
                com.Parameters.AddWithValue("_mname", xofficials.mname);
                com.Parameters.AddWithValue("_lname", xofficials.lname);
                com.Parameters.AddWithValue("_ext", xofficials.ext);
                com.Parameters.AddWithValue("_position", xofficials.position);
                com.Parameters.AddWithValue("_chairmanship", xofficials.chairmanship);
                com.Parameters.AddWithValue("_contact", xofficials.contact);
                com.Parameters.AddWithValue("_purok", xofficials.purok);
                com.Parameters.AddWithValue("_term_start", xofficials.term_start);
                com.Parameters.AddWithValue("_term_end", xofficials.term_end);
               
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }
        //Update Officials
        public async Task<int> UpdateOfficials(officials xofficials)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateOfficials", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_off_id", xofficials.off_id);
                com.Parameters.AddWithValue("_photo", xofficials.photo);
                com.Parameters.AddWithValue("_fname", xofficials.fname);
                com.Parameters.AddWithValue("_mname", xofficials.mname);
                com.Parameters.AddWithValue("_lname", xofficials.lname);
                com.Parameters.AddWithValue("_ext", xofficials.ext);
                com.Parameters.AddWithValue("_position", xofficials.position);
                com.Parameters.AddWithValue("_chairmanship", xofficials.chairmanship);
                com.Parameters.AddWithValue("_contact", xofficials.contact);
                com.Parameters.AddWithValue("_purok", xofficials.purok);
                com.Parameters.AddWithValue("_term_start", xofficials.term_start);
                com.Parameters.AddWithValue("_term_end", xofficials.term_end);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }

        //Delete Officials
        public async Task<int> DeleteOfficials(officials xofficials)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("DeleteOfficials", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("_off_id", xofficials.off_id);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }

        //Search Officials
        public async Task<List<officials>> SearchOfficials(string search)
        {
            List<officials> xofficials = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchOfficials", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xofficials.Add(new officials
                    {
                        off_id = rdr["off_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        position = rdr["position"].ToString(),
                        chairmanship = rdr["chairmanship"].ToString(),
                        contact = rdr["contact"].ToString(),
                        purok = rdr["purok"].ToString(),
                        term_start = Convert.ToDateTime(rdr["term_start"]).Date,
                        term_end = Convert.ToDateTime(rdr["term_end"]).Date,
                    });
                }
            }
            return xofficials;
        }
    }
}
