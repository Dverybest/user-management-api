using System.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TestApi.Data;
using TestApi.Dtos;

namespace TestApi.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper;
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
            _dapper = new DataContextDapper(config);
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistrationDto userRegistrationDto)
        {
            string findUserSql = "SELECT [Email] FROM TutorialAppSchema.Auth";
            IEnumerable<string> existingUsers = _dapper.LoadData<string>(findUserSql);
            if (!existingUsers.Any())
            {
                HashPassword(userRegistrationDto.Password, out byte[] saltPassword, out byte[] passwordHash);
                string sqlAddAuth = @"
                    INSERT INTO TutorialAppSchema.Auth(
                        [Email],
                        [PasswordHash],
                        [PasswordSalt]
                    )VALUES(
                        @Email,
                        @PasswordHash,
                        @PasswordSalt
                    )
                ";

                SqlParameter emailParam = new("@Email", SqlDbType.NVarChar)
                {
                    Value = userRegistrationDto.Email
                };
                SqlParameter passwordHashParam = new("@PasswordHash", SqlDbType.VarBinary)
                {
                    Value = saltPassword
                };
                SqlParameter passwordSaltParam = new("@PasswordSalt", SqlDbType.VarBinary)
                {
                    Value = passwordHash
                };
                List<SqlParameter> sqlParameters = [emailParam, passwordSaltParam, passwordHashParam];

                if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                {
                    return Ok();
                }
                throw new Exception("Failed to register user.");
            }
            throw new Exception("User already Exist.");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            return Ok();
        }

        public void HashPassword(string password, out byte[] saltPassword, out byte[] passwordHash)
        {
            saltPassword = new byte[128 / 8];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetNonZeroBytes(saltPassword);
            }
            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey")
            + Convert.ToBase64String(saltPassword);

            passwordHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000000,
                numBytesRequested: 256 / 8
            );
        }
    }
}