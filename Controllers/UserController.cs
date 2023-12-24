using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Dtos;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContextDapper _dataContextDapper;
        public UserController(IConfiguration config)
        {
            _dataContextDapper = new DataContextDapper(config);
        }
        [HttpGet("TestConnection")]
        public DateTime TestConnection()
        {
            return _dataContextDapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
        }

        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            string sql = @"
                SELECT 
                    * 
                FROM TutorialAppSchema.Users 
            ";
            IEnumerable<User> users = _dataContextDapper.LoadData<User>(sql);
            return users;
        }

        [HttpGet("GetUser/{userId}")]
        public User GetUser(string userId)
        {
            string sql = @"
                SELECT 
                    * 
                FROM TutorialAppSchema.Users 
                    WHERE UserId=
            " + userId.ToString();
            User user = _dataContextDapper.LoadDataSingle<User>(sql);
            return user;
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser(User user)
        {
            string sql = @" 
            UPDATE TutorialAppSchema.Users
            SET " +
                $"[FirstName]='{user.FirstName}'," +
                $"[LastName]='{user.LastName}'," +
                $"[Gender]='{user.Gender}'," +
                $"[Email]='{user.Email}'," +
                $"[Active]='{user.Active}' " +
            $"WHERE UserId={user.UserId}";
            Console.WriteLine(sql);
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to update user");
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser(AddUserDto user)
        {
            string sql = @"
            INSERT INTO TutorialAppSchema.Users(
              [FirstName],
              [LastName],
              [Gender],
              [Email],
              [Active]
              )VALUES(" +
                $"'{user.FirstName}'," +
                $"'{user.LastName}'," +
                $"'{user.Gender}'," +
                $"'{user.Email}'," +
                $"'{user.Active}')";

            Console.WriteLine(sql);

            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to add user");
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            string sql = @"
                DELETE FROM TutorialAppSchema.Users 
                    WHERE UserId =
            " + userId.ToString();

            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete user");
        }
    }
}
