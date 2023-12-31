namespace TrybeGames;

public class TrybeGamesDatabase
{
    public List<Game> Games = new List<Game>();

    public List<GameStudio> GameStudios = new List<GameStudio>();

    public List<Player> Players = new List<Player>();

    // 4. Crie a funcionalidade de buscar jogos desenvolvidos por um estúdio de jogos
    public List<Game> GetGamesDevelopedBy(GameStudio gameStudio)
    {
        var filtredGamesList = (
            from game in Games
            where game.DeveloperStudio == gameStudio.Id
            select game
        ).ToList();

        return filtredGamesList;
    }

    // 5. Crie a funcionalidade de buscar jogos jogados por uma pessoa jogadora
    public List<Game> GetGamesPlayedBy(Player player)
    {
        var filtredGamesList = (
          from game in Games
          from gamePlayer in game.Players
          where gamePlayer == player.Id
          select game
        ).ToList();

        return filtredGamesList;
    }


    // 6. Crie a funcionalidade de buscar jogos comprados por uma pessoa jogadora
    public List<Game> GetGamesOwnedBy(Player playerEntry)
    {
        var filtredGamesList = (
            from gamePlayer in playerEntry.GamesOwned
            from game in Games
            where gamePlayer == game.Id
            select game
        ).ToList();
        return filtredGamesList;
    }


    // 7. Crie a funcionalidade de buscar todos os jogos junto do nome do estúdio desenvolvedor
    public List<GameWithStudio> GetGamesWithStudio()
    {
        var gamesWithStudio = (
            from game in Games
            join studio in GameStudios
            on game.DeveloperStudio equals studio.Id
            select new GameWithStudio
            {
                GameName = game.Name,
                StudioName = studio.Name,
                NumberOfPlayers = game.Players.Count,
            }
        ).ToList();

        return gamesWithStudio;
    }

    // 8. Crie a funcionalidade de buscar todos os diferentes Tipos de jogos dentre os jogos cadastrados
    public List<GameType> GetGameTypes()
    {
        var gameTypes = (
            from game in Games
            group game by game.GameType into gameType
            select gameType.Key
        ).ToList();
        return gameTypes;
    }

    // 9. Crie a funcionalidade de buscar todos os estúdios de jogos junto dos seus jogos desenvolvidos com suas pessoas jogadoras
    public List<StudioGamesPlayers> GetStudiosWithGamesAndPlayers()
    {
        var studiosWithGamesAndPlayers = (
            from gameStudio in GameStudios
            select new StudioGamesPlayers
            {
                GameStudioName = gameStudio.Name,
                Games = (
                    from game in Games
                    where game.DeveloperStudio == gameStudio.Id
                    select new GamePlayer
                    {
                        GameName = game.Name,
                        Players = (
                            from player in Players
                            join gamePlayer in game.Players
                            on player.Id equals gamePlayer
                            select player
                        ).ToList()
                    }
                ).ToList()
            }

        ).ToList();

        return studiosWithGamesAndPlayers;
    }

}
