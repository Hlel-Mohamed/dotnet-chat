using dotnet_chat.Models;
using System.Linq;

namespace dotnet_chat.Data
{
    /// <summary>
    /// UserRepository is a class that implements the IUserRepository interface.
    /// It provides methods for interacting with the User data in the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        /// <summary>
        /// The constructor for the UserRepository class.
        /// </summary>
        /// <param name="context">An instance of UserContext to interact with the database.</param>
        public UserRepository(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create is a method that adds a new User to the database.
        /// </summary>
        /// <param name="user">The User object that needs to be added to the database.</param>
        /// <returns>The User object that was added to the database, with the ID property set to the generated ID.</returns>
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        /// <summary>
        /// GetByEmail is a method that retrieves a User from the database based on their email.
        /// </summary>
        /// <param name="email">The email of the User that needs to be retrieved.</param>
        /// <returns>The User object that matches the provided email, or null if no such User exists.</returns>
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// GetById is a method that retrieves a User from the database based on their ID.
        /// </summary>
        /// <param name="id">The ID of the User that needs to be retrieved.</param>
        /// <returns>The User object that matches the provided ID, or null if no such User exists.</returns>
        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}