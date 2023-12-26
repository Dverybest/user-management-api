using Microsoft.AspNetCore.Http.HttpResults;
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
        [HttpGet("UserSalary/{userId}")]
        public UserSalary GetUserSalary(string userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}";
            UserSalary userSalary = _dataContextDapper.LoadDataSingle<UserSalary>(sql);
            return userSalary;
        }
        [HttpPost("UserSalary")]
        public IActionResult AddUserSalary(UserSalary userSalary)
        {
            string sql = @"INSERT INTO TutorialAppSchema.UserSalary(
                [UserId],
                [Salary]
            )VALUES("
            + $"'{userSalary.UserId}',"
            + $"'{userSalary.Salary}'"
            + ")";
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to add user salary");
        }
        [HttpPut("UserSalary")]
        public IActionResult EditUserSalary(UserSalary userSalary)
        {
            string sql = @"UPDATE TutorialAppSchema.UserSalary
                SET "
                 + $"[Salary]={userSalary.Salary}"
                 + $"WHERE UserId = {userSalary.UserId}";
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to update user salary");
        }
        [HttpDelete("UserSalary/{userId}")]
        public IActionResult DeleteUserSalary(string userId)
        {
            string sql = $"DELETE FROM TutorialAppSchema.UserSalary WHERE UserId = {userId}";
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete user job info");
        }
        [HttpGet("UserJobInfo/{userId}")]
        public UserJobInfo GetUserJobInfo(string userId)
        {
            string sql = $"SELECT * FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}";
            UserJobInfo userJobInfo = _dataContextDapper.LoadDataSingle<UserJobInfo>(sql);
            return userJobInfo;
        }
        [HttpPost("UserJobInfo")]
        public IActionResult AddUserJobInfo(UserJobInfo userJobInfo)
        {
            string sql = @"INSERT INTO TutorialAppSchema.UserJobInfo(
                [UserId],
                [JobTitle],
                [Department]
            )VALUES("
             + $"'{userJobInfo.UserId}'"
            + $"'{userJobInfo.JobTitle}',"
            + $"'{userJobInfo.Department}'"
            + ")";
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to add user job info");
        }
        [HttpPut("UserJobInfo")]
        public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
        {
            string sql = @"UPDATE TutorialAppSchema.UserJobInfo
                SET "
                 + $"[JobTitle]={userJobInfo.JobTitle},"
                  + $"[JobTitle]={userJobInfo.Department} "
                 + $"WHERE UserId = {userJobInfo.UserId}";
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to update user job info");
        }
        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo(string userId)
        {
            string sql = $"DELETE FROM TutorialAppSchema.UserJobInfo WHERE UserId = {userId}";
            if (_dataContextDapper.ExecuteSql(sql))
            {
                return Ok();
            }
            throw new Exception("Failed to delete user job info");
        }
    }
}
