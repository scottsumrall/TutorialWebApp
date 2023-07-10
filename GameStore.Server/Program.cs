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

group.MapPost("/", (Game game) =>
{
    game.Id = games.Max(game => game.Id) + 1;
    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new { id = game.Id }, game);
});

app.Run();
