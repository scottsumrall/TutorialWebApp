using GameStore.Server.Data.Configurations;
using GameStore.Server.Models;
using Microsoft.EntityFrameworkCore;

List<Game> games = new()
{
	new Game()
	{
		Id = 1,
		Name = "Street Fighter II",
		Genre = "Fighting",
		Price = 19.99M,
		ReleaseDate = new DateTime(1991, 2, 1)
	},
	new Game()
	{
		Id = 2,
		Name = "Final Fantasy XIV",
		Genre = "RolePlaying",
		Price = 59.99M,
		ReleaseDate = new DateTime(2010, 9, 30)
	},
	new Game()
	{
		Id = 3,
		Name = "FIFA 23",
		Genre = "Sports",
		Price = 69.99M,
		ReleaseDate = new DateTime(2022, 9, 7)
	}
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
	builder.WithOrigins("http://localhost:5124")
					.AllowAnyHeader()
					.AllowAnyMethod();
}));

var conString = builder.Configuration.GetConnectionString("GameStoreContext");

// Regester the dbContext (GameStoreContext) for dependency injection
builder.Services.AddSqlServer<GameStoreContext>(conString);

var app = builder.Build();

app.UseCors();

var group = app.MapGroup("/games")
	.WithParameterValidation();

// GET /games
// Set our get to retrieve from GameStoreContext asynchronously
// AsNoTracking for read only list, ToListAsync to return a list
group.MapGet("/", async (GameStoreContext context) =>
	await context.Games.AsNoTracking().ToListAsync());

// GET /games/{id}
group.MapGet("/{id}", async (int id, GameStoreContext context) =>
{
	Game? game = await context.Games.FindAsync(id);

	//if game is null, return not found results object
	if (game is null)
	{
		return Results.NotFound(0);
	}

	//if game is found, return Ok results object and pass in the game object
	return Results.Ok(game);
}).WithName("GetGame");

// POST /games
group.MapPost("/", async (Game game, GameStoreContext context) =>
{
	context.Games.Add(game);
	await context.SaveChangesAsync();

	return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});


// PUT /games/{id}
group.MapPut("/{id}", async (int id, Game updatedGame, GameStoreContext context) =>
{
	var rowsAffected = await context.Games.Where(game => game.Id == id)
			.ExecuteUpdateAsync(updates =>
				updates.SetProperty(game => game.Name, updatedGame.Name)
						.SetProperty(game => game.Genre, updatedGame.Genre)
						.SetProperty(game => game.Price, updatedGame.Price)
						.SetProperty(game => game.ReleaseDate, updatedGame.ReleaseDate));

	return rowsAffected == 0 ? Results.NotFound() : Results.NoContent();
});

group.MapDelete("/{id}", async (int id, GameStoreContext context) =>
{
	var rowsAffected = await context.Games.Where(game => game.Id == id)
			.ExecuteDeleteAsync();

	return rowsAffected == 0 ? Results.NotFound() : Results.NoContent();
});

app.Run();
