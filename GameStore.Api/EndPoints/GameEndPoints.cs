using GameStore.Api.Entities;

namespace GameStore.Api.EndPoints
{

    public static class GameEndPoints
    {
        const string GetGameEndPoint = "GetGame";
       static List<Game> games = new()
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

        public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes){

var group = routes.MapGroup("/games")
.WithParameterValidation();
group.MapGet("/", () => games);             


group.MapGet("/{id}", (int id) =>
 {
      Game? game = games.Find(game => game.Id == id);
      if (game is null)
      {
            return Results.NotFound();
      }

      return Results.Ok(game);
 })
 .WithName(GetGameEndPoint);

group.MapPost("/", (Game game) =>
{
      game.Id = games.Max(game => game.Id) + 1;
      games.Add(game);

      return Results.CreatedAtRoute(GetGameEndPoint, new {id = game.Id}, game);
});

group.MapPut("/{id}", (int id, Game updatedGame ) =>
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

 group.MapDelete("/{id}", (int id) =>
 {
      Game? game = games.Find(game => game.Id == id);
      if (game is not null)
      {
            games.Remove(game);
      }

      return Results.NoContent();
 });
   return group;

        }
    }
}