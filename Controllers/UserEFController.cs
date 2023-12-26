using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Dtos;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserEFController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserEFController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AddUserDto, User>();
                }
            ));
        }


        [HttpGet("Users")]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        [HttpGet("GetUser/{userId}")]
        public User GetUser(int userId)
        {
            return _userRepository.GetSingleUser(userId);
        }

        [HttpPut("User")]
        public IActionResult EditUser(User userDto)
        {
            User user = _userRepository.GetSingleUser(userDto.UserId);
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Gender = userDto.Gender;
            user.Active = userDto.Active;
            user.Email = userDto.Email;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to update user");

        }

        [HttpPost("User")]
        public IActionResult AddUser(AddUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            _userRepository.AddEntity(user);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to add user");

        }

        [HttpDelete("User/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            User? user = _userRepository.GetSingleUser(userId);
            _userRepository.RemoveEntity(user);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to delete user");
        }

        [HttpGet("UserSalary/{userId}")]
        public UserSalary GetUserSalary(int userId)
        {
            return _userRepository.GetSingleUserSalary(userId);
        }

        [HttpPost("UserSalary")]
        public IActionResult PostUserSalary(UserSalary userSalaryDto)
        {
            _userRepository.AddEntity(userSalaryDto);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to add user salary");
        }

        [HttpPut("UserSalary")]
        public IActionResult PutUserSalary(UserSalary userSalaryDto)
        {
            UserSalary userSalary = _userRepository.GetSingleUserSalary(userSalaryDto.UserId);
            userSalary.Salary = userSalaryDto.Salary;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to edit user salary");
        }

        [HttpDelete("UserSalary/{userId}")]
        public IActionResult DeleteUserSalary(int userId)
        {
            UserSalary userSalary = _userRepository.GetSingleUserSalary(userId);
            _userRepository.RemoveEntity(userSalary);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to delete user salary");
        }

        [HttpGet("UserJobInfo/{userId}")]
        public UserJobInfo GetUserJobInfo(int userId)
        {
            return _userRepository.GetSingleUserJobInfo(userId);
        }

        [HttpPost("UserJobInfo")]
        public IActionResult PostUserJobInfo(UserJobInfo userJobInfo)
        {
            _userRepository.AddEntity(userJobInfo);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to add user job info");
        }

        [HttpPut("UserJobInfo")]
        public IActionResult PutUserJobInfo(UserJobInfo userJobInfoDto)
        {
            UserJobInfo userJobInfo = _userRepository.GetSingleUserJobInfo(userJobInfoDto.UserId);
            userJobInfo.Department = userJobInfoDto.Department;
            userJobInfo.JobTitle = userJobInfo.JobTitle;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to edit user job info");
        }

        [HttpDelete("UserJobInfo/{userId}")]
        public IActionResult DeleteUserJobInfo(int userId)
        {
            UserJobInfo userJobInfo = _userRepository.GetSingleUserJobInfo(userId);
            _userRepository.RemoveEntity(userJobInfo);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to delete user job info");
        }
    }
}
