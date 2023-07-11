using TutorialWebApp.Models;
using System.Net.Http.Json;

namespace TutorialWebApp
{
	public class GameClient
	{
		private readonly HttpClient httpClient;

		public GameClient(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<Game[]?> GetGamesAsync()
		{
			//return games.ToArray();

			// It takes time to get response from API, returns a task so must be async
			return await httpClient.GetFromJsonAsync<Game[]>("games");
		}

		public async Task AddGameAsync(Game game)
		{
			await httpClient.PostAsJsonAsync("games", game);
		}

		public async Task<Game> GetGameAsync(int id)
		{
			return await httpClient.GetFromJsonAsync<Game>($"games/{id}") 
				?? throw new Exception("Could not find game!");
		}

		public async Task UpdateGameAsync(Game updatedGame)
		{
			await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

			/*
			Game existingGame = GetGame(updatedGame.Id);
			existingGame.Name = updatedGame.Name;
			existingGame.Genre = updatedGame.Genre;
			existingGame.Price = updatedGame.Price;
			existingGame.ReleaseDate = updatedGame.ReleaseDate;
			*/
		}

		public async Task DeleteGameAsync(int id)
		{
			await httpClient.DeleteAsync($"games/{id}");
		}
	}
}
