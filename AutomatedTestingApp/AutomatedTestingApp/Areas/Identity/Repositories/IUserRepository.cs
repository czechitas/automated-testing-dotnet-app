using AutomatedTestingApp.Areas.Identity.Models;

namespace AutomatedTestingApp.Areas.Identity.Repositories;

public interface IUserRepository
{
    IEnumerable<IdentityUser> GetUsers();
    IdentityUser? GetUserById(Guid userId);
    Task<IdentityUser?> GetUserByUsernameAsync(string username);
    void CreateUser(IdentityUser user);
    void UpdateUser(IdentityUser user);
    void DeleteUser(IdentityUser user);
    void Save();
}