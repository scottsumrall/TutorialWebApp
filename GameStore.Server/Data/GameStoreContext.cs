using Microsoft.EntityFrameworkCore;
using TutorialWebApp.Models;

namespace GameStore.Server.NewFolder
{
	public class GameStoreContext : DbContext
	{
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
            
        }

        public DbSet<Game> Games => Set<Game>();
    }
}
