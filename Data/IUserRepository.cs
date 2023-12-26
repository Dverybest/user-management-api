using TestApi.Models;

namespace TestApi.Data
{
    public interface IUserRepository
    {
        public bool SaveChanges();

        public void AddEntity<T>(T entity);

        public void RemoveEntity<T>(T entity);

        public IEnumerable<User> GetUsers();

        public User GetSingleUser(int userId);

        public UserSalary GetSingleUserSalary(int userId);

        public UserJobInfo GetSingleUserJobInfo(int userId);

    }
}