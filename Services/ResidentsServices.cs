using Gbms.Class;
using Gbms.Models;
using Gbms.Shared;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Gbms.Server.Services
{
    public class ResidentsServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;

        public ResidentsServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Residents
        public async Task<List<residents>> Residents()
        {

            List<residents> xresidents = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ViewResidents", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xresidents.Add(new residents
                    {
                        res_id = rdr["res_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        house_id = rdr["house_id"].ToString(),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        ext = rdr["ext"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        household = rdr["household"].ToString(),
                        purok = rdr["purok"].ToString(),
                        birthplace = rdr["birthplace"].ToString(),
                        birthdate = Convert.ToDateTime(rdr["birthdate"]),
                        age = Convert.ToInt32(rdr["age"]),
                        gender = rdr["gender"].ToString(),
                        civil = rdr["civil"].ToString(),
                        citizenship = rdr["citizenship"].ToString(),
                        occupation = rdr["occupation"].ToString(),
                        status = rdr["status"].ToString()
                    });
                }
            }
            return xresidents;
        }

        //Residents Report
        public async Task<List<residents>> ResidentReport()
        {

            List<residents> xresidents = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("ReportResidents", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xresidents.Add(new residents
                    {
                        res_id = rdr["res_id"].ToString(),
                        house_id = rdr["house_id"].ToString(),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        ext = rdr["ext"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        household = rdr["household"].ToString(),
                        purok = rdr["purok"].ToString(),
                        birthplace = rdr["birthplace"].ToString(),
                        birthdate = Convert.ToDateTime(rdr["birthdate"]),
                        age = Convert.ToInt32(rdr["age"]),
                        gender = rdr["gender"].ToString(),
                        civil = rdr["civil"].ToString(),
                        citizenship = rdr["citizenship"].ToString(),
                        occupation = rdr["occupation"].ToString(),
                        status = rdr["status"].ToString()
                    });
                }
            }
            return xresidents;
        }

        //Insert Residents
        public async Task<int> AddResidents(residents xresidents)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("InsertResidents", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                com.Parameters.AddWithValue("_res_id", xresidents.res_id);
                com.Parameters.AddWithValue("_photo", xresidents.photo);
                com.Parameters.AddWithValue("_house_id", xresidents.house_id);
                com.Parameters.AddWithValue("_fname", xresidents.fname);
                com.Parameters.AddWithValue("_mname", xresidents.mname);
                com.Parameters.AddWithValue("_lname", xresidents.lname);
                com.Parameters.AddWithValue("_ext", xresidents.ext);
                com.Parameters.AddWithValue("_birthplace", xresidents.birthplace);
                com.Parameters.AddWithValue("_birthdate", xresidents.birthdate);
                com.Parameters.AddWithValue("_age", xresidents.age);
                com.Parameters.AddWithValue("_gender", xresidents.gender);
                com.Parameters.AddWithValue("_civil", xresidents.civil);
                com.Parameters.AddWithValue("_citizenship", xresidents.citizenship);
                com.Parameters.AddWithValue("_occupation", xresidents.occupation);
                com.Parameters.AddWithValue("_status", xresidents.status);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);

            }

            return i;
        }

        //Update Residents
        public async Task<int> UpdateResidents(residents xresidents)
        {
            int i = 0;
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("UpdateResidents", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("_res_id", xresidents.res_id);
                com.Parameters.AddWithValue("_photo", xresidents.photo);
                com.Parameters.AddWithValue("_house_id", xresidents.house_id);
                com.Parameters.AddWithValue("_fname", xresidents.fname);
                com.Parameters.AddWithValue("_mname", xresidents.mname);
                com.Parameters.AddWithValue("_lname", xresidents.lname);
                com.Parameters.AddWithValue("_ext", xresidents.ext);
                com.Parameters.AddWithValue("_birthplace", xresidents.birthplace);
                com.Parameters.AddWithValue("_birthdate", xresidents.birthdate);
                com.Parameters.AddWithValue("_age", xresidents.age);
                com.Parameters.AddWithValue("_gender", xresidents.gender);
                com.Parameters.AddWithValue("_civil", xresidents.civil);
                com.Parameters.AddWithValue("_citizenship", xresidents.citizenship);
                com.Parameters.AddWithValue("_occupation", xresidents.occupation);
                com.Parameters.AddWithValue("_status", xresidents.status);
                i = await com.ExecuteNonQueryAsync().ConfigureAwait(false);
            }

            return i;
        }


        //Count Residents
        public async Task<int> TotalResidents()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM residents", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Count Male
        public async Task<int> TotalMale()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM residents WHERE gender = 'Male'", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Count Female
        public async Task<int> TotalFemale()
        {
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT COUNT(*) FROM residents WHERE gender = 'Female'", con)
                {
                    CommandType = CommandType.Text,
                };
                return Convert.ToInt32(await com.ExecuteScalarAsync().ConfigureAwait(false));
            }
        }

        //Search Residents
        public async Task<List<residents>> SearchResidents(string search)
        {
            List<residents> xresidents = new();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SearchResidents", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("search", search);
                com.Parameters.AddWithValue("@searchWildcard", $"{search}%");

                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xresidents.Add(new residents
                    {
                        res_id = rdr["res_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        house_id = rdr["house_id"].ToString(),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        ext = rdr["ext"].ToString(),
                        fullname = rdr["fullname"].ToString(),
                        household = rdr["household"].ToString(),
                        purok = rdr["purok"].ToString(),
                        birthplace = rdr["birthplace"].ToString(),
                        birthdate = Convert.ToDateTime(rdr["birthdate"]),
                        age = Convert.ToInt32(rdr["age"]),
                        gender = rdr["gender"].ToString(),
                        civil = rdr["civil"].ToString(),
                        citizenship = rdr["citizenship"].ToString(),
                        occupation = rdr["occupation"].ToString(),
                        status = rdr["status"].ToString(),
                    });
                }
            }
            return xresidents;
        }

    }
}
