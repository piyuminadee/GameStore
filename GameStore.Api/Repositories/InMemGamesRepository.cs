using GameStore.Api.Entities;

namespace GameStore.Api.Repositories
{
    public class InMemGamesRepository
    {
        static readonly List<Game> games = new()
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

        public IEnumerable<Game> GetAll()
        {
            return games;
        }



        public Game? Get(int id)
        {
            return games.Find(game => game.Id == id);
        }

        public void Create(Game game)
        {
            game.Id = games.Max(game => game.Id) + 1;
            games.Add(game);
        }

        public void Update(Game updatedGame)
        {
            var index = games.FindIndex(game => game.Id == updatedGame.Id);
            games[index] = updatedGame;
        }

        public void Delete(int id)
        {
            var index = games.FindIndex(game => game.Id == id);
            games.RemoveAt(index);
        }

    }
}
