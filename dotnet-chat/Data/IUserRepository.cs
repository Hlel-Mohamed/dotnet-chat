using dotnet_chat.Models;

namespace dotnet_chat.Data;

public interface IUserRepository
{
    User Create(User user);
    User GetByEmail(string email);
    User GetById(int id);
}