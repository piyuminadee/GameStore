using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.EndPoints
{

      public static class GameEndPoints
      {
            const string GetGameEndPoint = "GetGame";

            public static RouteGroupBuilder MapGamesEndPoints(this IEndpointRouteBuilder routes)
            {
                  InMemGamesRepository repository = new();

                  var group = routes.MapGroup("/games")
                  .WithParameterValidation();

                  group.MapGet("/", () => repository.GetAll());


                  group.MapGet("/{id}", (int id) =>
                   {
                         Game? game = repository.Get(id);

                         return game is not null ? Results.Ok(game) : Results.NotFound();
                         
                   })
                   .WithName(GetGameEndPoint);

                  group.MapPost("/", (Game game) =>
                  {
                        repository.Create(game);

                        return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);
                  });

                  group.MapPut("/{id}", (int id, Game updatedGame) =>
                   {
                         Game? existingGame = repository.Get(id);
                         if (existingGame is null)
                         {
                               return Results.NotFound();
                         }

                         existingGame.Genre = updatedGame.Genre;
                         existingGame.Name = updatedGame.Name;
                         existingGame.Price = updatedGame.Price;
                         existingGame.ReleaseData = updatedGame.ReleaseData;
                         existingGame.ImageUri = updatedGame.ImageUri;

                         repository.Update(existingGame); 
                         return Results.NoContent();
                   });

                  group.MapDelete("/{id}", (int id) =>
                  {
                        Game? game = repository.Get(id);
                        if (game is not null)
                        {
                              repository.Delete(id);
                        }

                        return Results.NoContent();
                  });
                  return group;

            }
      }
}