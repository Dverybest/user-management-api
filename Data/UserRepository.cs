using TestApi.Data;
using TestApi.Models;

namespace TestApi.Data
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContextEF _entityFramework;

        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entity)
        {
            if (entity != null)
            {
                _entityFramework.Add(entity);
            }
        }
        public void RemoveEntity<T>(T entity)
        {
            if (entity != null)
            {
                _entityFramework.Remove(entity);
            }
        }
        public IEnumerable<User> GetUsers()
        {
            return _entityFramework.Users.ToList();
        }

        public User GetSingleUser(int userId)
        {
            User? user = _entityFramework.Users.Where(user => user.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            throw new Exception("Fail to find user");
        }

        public UserSalary GetSingleUserSalary(int userId)
        {
            UserSalary? userSalary = _entityFramework.UserSalaries.Where(user => user.UserId == userId).FirstOrDefault();
            if (userSalary != null)
            {
                return userSalary;
            }
            throw new Exception("Fail to get user");
        }

        public UserJobInfo GetSingleUserJobInfo(int userId)
        {
            UserJobInfo? userJobInfo = _entityFramework.UserJobInfos.Where(user => user.UserId == userId).FirstOrDefault();
            if (userJobInfo != null)
            {
                return userJobInfo;
            }
            throw new Exception("Fail to get user");
        }
    }
}