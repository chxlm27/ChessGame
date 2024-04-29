using Gamee.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace Gamee.Chess
{
    public class ChessGame : AGame
    {
        private IBoard board;
        private Referee referee;
        public Context GameContext { get; set; } = new Context { Layout = new ChessLayout() };

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public override void Initialize(IBoard _board, Referee _referee)
        {
            board = _board;
            referee = _referee;

            board.Initialize();
            referee.Initialize();

            // Connecting Board's MoveProposed to Referee's handler
            board.MoveProposed += referee.OnMoveProposed;
            // Connecting Referee's GameContextChanged to Board's event handler
            referee.GameContextChanged += board.OnGameContextChanged;
            // Ensuring that the referee's context change triggers game-wide updates
            referee.GameContextChanged += OnRefereeGameContextChanged;
            // Saving the game whenever the state changes
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
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List
                <JsonConverter> { new ALayoutConverter(), new CoordinateConverter() }
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
                    Converters = new List
                    <JsonConverter> { new ALayoutConverter(), new CoordinateConverter() }
                };
                string json = File.ReadAllText(filePath);
                GameContext = JsonConvert.DeserializeObject
                <Context>(json, settings);
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
