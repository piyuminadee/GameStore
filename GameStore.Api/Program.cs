using GameStore.Api.Entities;
const string GetGameEndPoint = "GetGame";

List<Game> games = new()
{
    new Game()
    {
          Genre = "Cars",
          Id= 1,
          Name = "Need For Speed",
          Price = 19.45M,
          ReleaseData = new DateTime(1890, 2 ,1),
          ImageUri = "https://placehold.co/100"
    },

    new Game()
    {
          Id= 2,
          Genre = "Action",
          Name = "Temple Run",
          Price = 9.65M,
          ReleaseData = new DateTime(1994, 5 ,3),
          ImageUri = "https://placehold.co/100"
    },
    new Game()
    {
          Id= 3,
          Genre = "Action",
          Name = "YCT",
          Price = 59.65M,
          ReleaseData = new DateTime(2010, 2 ,8),
          ImageUri = "https://placehold.co/100"
    },
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/games", () => games);
app.MapGet("/games/{id}", (int id) =>
 {
      Game? game = games.Find(game => game.Id == id);
      if (game is null)
      {
            return Results.NotFound();
      }

      return Results.Ok(game);
 })
 .WithName(GetGameEndPoint);

app.MapPost("/games", (Game game) =>
{
      game.Id = games.Max(game => game.Id) + 1;
      games.Add(game);

      return Results.CreatedAtRoute(GetGameEndPoint, new {id = game.Id}, game);
});

app.MapPut("/games/{id}", (int id, Game updatedGame ) =>
 {
      Game? existingGame = games.Find(game => game.Id == id);
      if (existingGame is null)
      {
            return Results.NotFound();
      }

      existingGame.Genre = updatedGame.Genre;
      existingGame.Name = updatedGame.Name;
      existingGame.Price = updatedGame.Price;
      existingGame.ReleaseData = updatedGame.ReleaseData;
      existingGame.ImageUri = updatedGame.ImageUri;
      

      return Results.NoContent();
 });
 


app.Run();
