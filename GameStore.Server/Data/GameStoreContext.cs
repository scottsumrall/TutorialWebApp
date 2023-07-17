using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GameStore.Server.Data.Configurations
{
	public class GameStoreContext : DbContext
	{
		public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
		{

		}

		// create a set that can translate instances of game into databse queries
		// Send GET, POST, PUT, and DELETE operations here
		public DbSet<Game> Games => Set<Game>();

		// Use configuration in assembly for any new models
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
