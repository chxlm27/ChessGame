using Gamee.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gamee.Checkers
{
    public class CheckersGame : AGame
    {
        private IBoard board;
        private Referee referee;
        public Context GameContext { get; set; } = new Context();

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public override void Initialize(IBoard _board, Referee _referee)
        {
            board = _board;
            referee = _referee;

            // Set the specific layout for checkers
            referee.SetLayout(new CheckersLayout());

            board.Initialize();
            referee.Initialize();

            board.MoveProposed += referee.OnMoveProposed;
            referee.GameContextChanged += board.OnGameContextChanged;
            referee.GameContextChanged += OnRefereeGameContextChanged;
            GameContext.StateChanged += () => SaveGame("current_game.json");
        }

        public override void Start()
        {
            referee.Start();
        }

        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContext = e.NewContext;
            GameContextChanged?.Invoke(this, e);
        }

        public override void SaveGame(string filePath)
        {
            if (GameContext == null)
            {
                Console.WriteLine("No game context available to save.");
                return;
            }

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List<JsonConverter> { new ALayoutConverter(new CheckersLayoutFactory()), new CoordinateConverter() }
            };


            string json = JsonConvert.SerializeObject(GameContext, settings);
            try
            {
                File.WriteAllText(filePath, json);
                Console.WriteLine($"Game saved successfully at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public override Context LoadGame(string filePath)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Converters = new List<JsonConverter>
            {
                new ALayoutConverter(new CheckersLayoutFactory()), // Pass the factory to the converter
                new CoordinateConverter()
            }
                };
                string json = File.ReadAllText(filePath);
                GameContext = JsonConvert.DeserializeObject<Context>(json, settings);
                GameContextChanged?.Invoke(this, new GameContextChangedEventArgs(GameContext));
                return GameContext;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                return null;
            }
        }
    }
}
