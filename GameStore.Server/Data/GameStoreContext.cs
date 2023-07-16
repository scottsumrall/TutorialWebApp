using Microsoft.EntityFrameworkCore;
using TutorialWebApp.Models;

namespace GameStore.Server.NewFolder
{
	public class GameStoreContext : DbContext
	{
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
            
        }

        //create a set that can translate instances of game into databse queries
        public DbSet<Game> Games => Set<Game>();
    }
}
