using GameStore.Api.EndPoints;
using GameStore.Api.Entities;



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndPoints();
             


app.Run();
