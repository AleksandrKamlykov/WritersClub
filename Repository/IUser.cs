using WritersClub.Models;

namespace WritersClub.Repository;

public interface IUser
{
    public Task<User> AddUser(User user);
    public Task<User> GetUserById(int id);
    public Task<User> GetUserByLogin(string login);
    public Task<User> UpdateUser(User user);
    public Task<User> DeleteUser(int id);
}