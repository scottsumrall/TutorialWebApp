﻿using TutorialWebApp.Models;

namespace TutorialWebApp
{
	public class GameClient
	{
		private readonly HttpClient httpClient;

		public GameClient(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		private readonly List<Game> games = new()
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

		public Game[] GetGames()
		{
			return games.ToArray();
		}

		public void AddGame(Game game)
		{
			game.Id = games.Max(game => game.Id) + 1;
			games.Add(game);
		}

		public Game GetGame(int id)
		{
			return games.Find(game => game.Id == id) ?? throw new Exception("Could not find game!");
		}

		public void UpdateGame(Game updatedGame)
		{
			Game existingGame = GetGame(updatedGame.Id);
			existingGame.Name = updatedGame.Name;
			existingGame.Genre = updatedGame.Genre;
			existingGame.Price = updatedGame.Price;
			existingGame.ReleaseDate = updatedGame.ReleaseDate;
		}

		public void DeleteGame(int id)
		{
			Game game = GetGame(id);
			games.Remove(game);
		}
	}
}
