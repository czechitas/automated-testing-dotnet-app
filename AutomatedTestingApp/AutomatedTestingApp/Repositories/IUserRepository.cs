using AutomatedTestingApp.Entity;

namespace AutomatedTestingApp.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
    User GetUserById(Guid userId);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);
    void Save();
}