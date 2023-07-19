using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Server.Data.Configurations
{
	// entity type configuration class for game
	public class GameConfiguration : IEntityTypeConfiguration<Game>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Game> builder)
		{
			builder.Property(game => game.Price)
				.HasPrecision(18, 2);
		}
	}
}
