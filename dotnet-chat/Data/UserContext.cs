using dotnet_chat.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_chat.Data
{
    /// <summary>
    /// UserContext is a DbContext class that represents a session with the database and can be used to query and save instances of the entities.
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// The constructor for the UserContext class.
        /// </summary>
        /// <param name="options">A DbContextOptions instance carrying the configuration settings for the DbContext.</param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet representing the Users table in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types exposed in DbSet properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically define extension methods on this object that allow you to configure aspects of the model that are specific to a given database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                // Configures an index for the Email property of the User entity to be unique.
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}