using Gbms.Class;
using Gbms.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gbms.Server.Services
{
    public class UserServices
    {
        private readonly AppDb _constring;
        public IConfiguration Configuration;
        private readonly AppSettings _appSetting;



        public UserServices(AppDb constring, IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _constring = constring;
            Configuration = configuration;
            _appSetting = appSettings.Value;
        }

        //View Users
        public async Task<List<user>> User()
        {

            List<user> xuser = new List<user>();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT * FROM user", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                while (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xuser.Add(new user
                    {
                        userid = rdr["user_id"].ToString(),
                        accounttype = rdr["accounttype"].ToString(),
                        name = rdr["name"].ToString(),
                        username = rdr["username"].ToString(),
                        password = rdr["password"].ToString(),
                    });
                }
            }
            return xuser;
        }


        //User Login
        public async Task<List<user>> Login(string user, string pwd)
        {
            List<user> xuser = new List<user>();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT * FROM user WHERE username = @user AND password = @pass", con)
                {
                    CommandType = CommandType.Text,
                };
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@user", user);
                com.Parameters.AddWithValue("@pass", pwd);
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                if (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xuser.Add(new user
                    {
                        userid = rdr["user_id"].ToString(),
                        accounttype = rdr["accounttype"].ToString(),
                        name = rdr["name"].ToString(),
                        username = rdr["username"].ToString(),
                        password = rdr["password"].ToString(),
                       
                    });
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                    new Claim (ClaimTypes.Name ,user),
                    new Claim(ClaimTypes.Role ,xuser[0].accounttype!),
                    new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    }),

                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    xuser[0].token = tokenHandler.WriteToken(token);
                }
                else
                {
                    xuser.Add(new user
                    {
                        userid = null,
                        accounttype = null,
                        name = null,
                        username = null,
                        password = null
                    });
                }
            }
            return xuser;
        }

        //Resident Login
        public async Task<List<residents>> ResLogin(string resid)
        {
            List<residents> xuser = new List<residents>();
            using (var con = new MySqlConnection(_constring.GetConnection()))
            {
                await con.OpenAsync().ConfigureAwait(false);
                var com = new MySqlCommand("SELECT * FROM residents WHERE res_id = @resid", con)
                {
                    CommandType = CommandType.Text,
                };
                com.Parameters.Clear();
              
                com.Parameters.AddWithValue("@resid", resid);
                var rdr = await com.ExecuteReaderAsync().ConfigureAwait(false);
                if (await rdr.ReadAsync().ConfigureAwait(false))
                {
                    xuser.Add(new residents
                    {
                        res_id = rdr["res_id"].ToString(),
                        photo = (byte[])rdr["photo"],
                        house_id = rdr["house_id"].ToString(),
                        fname = rdr["fname"].ToString(),
                        mname = rdr["mname"].ToString(),
                        lname = rdr["lname"].ToString(),
                        ext = rdr["ext"].ToString(),
                        //fullname = rdr["fullname"].ToString(),
                        //household = rdr["household"].ToString(),
                        //purok = rdr["purok"].ToString(),
                        birthplace = rdr["birthplace"].ToString(),
                        birthdate = Convert.ToDateTime(rdr["birthdate"]),
                        age = Convert.ToInt32(rdr["age"]),
                        gender = rdr["gender"].ToString(),
                        civil = rdr["civil"].ToString(),
                        citizenship = rdr["citizenship"].ToString(),
                        occupation = rdr["occupation"].ToString(),
                        status = rdr["status"].ToString()
                    });

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                    new Claim (ClaimTypes.Name ,resid),
                    new Claim(ClaimTypes.Role ,xuser[0].res_id!),
                    new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    }),

                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    xuser[0].token = tokenHandler.WriteToken(token);
                }
                else
                {
                    xuser.Add(new residents
                    {
                       fname = null,
                       mname = null,
                       lname = null,
                    });
                }
            }
            return xuser;
        }

    }
}