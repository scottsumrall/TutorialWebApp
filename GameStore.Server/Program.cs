using TutorialWebApp.Models;

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
var app = builder.Build();

var group = app.MapGroup("/games");

group.MapGet("/", () => games);

// GET /games
group.MapGet("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    //if game is null, return not found results object
    if (game is null)
    {
        return Results.NotFound(0);
    }

    //if game is found, return Ok results object and pass in the game object
    return Results.Ok(game);
}).WithName("GetGame");

// PUT /games
group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

// PUT /games/{id}
group.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        updatedGame.Id = id;
        games.Add(updatedGame);
        return Results.CreatedAtRoute("GetGame", new { id = updatedGame.Id }, updatedGame);
    }
    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

group.MapDelete("/{id}", (int id) =>
{
    Game? game = games.Find(game => game.Id == id);

    if (game is null)
    {
        // Return not found or no content
        // If the game cant be found, then the client intent to delete is already applicable, so no content works
        // Not found might be easier for debug though.

        return Results.NotFound();
        //return Results.NoContent();

    }
    games.Remove(game);

    return Results.NoContent();
});

app.Run();
