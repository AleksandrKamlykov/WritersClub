using WritersClub.Models;

namespace WritersClub.Repository;

public interface IUser
{
    public Task<User> AddUser(User user);
    public Task<User> GetUserById(int id);
    public Task<User> GetUserByLogin(string login,int id = 0);
    public Task<User> UpdateUser(User user);
    public Task<User> DeleteUser(int id);
    public Task<IEnumerable<User>> GetAllUsers();
    Task<bool> IsUserNameAvailable(string login);
}