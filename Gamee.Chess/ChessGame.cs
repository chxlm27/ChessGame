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
        private ALayout chessLayout; 

        public Context GameContext { get; set; } = new Context();

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public override void Initialize(IBoard _board, Referee _referee)
        {
            board = _board;
            referee = _referee;

            // Create and set the specific layout for chess
            chessLayout = new ChessLayout();
            referee.SetLayout(chessLayout);
            referee.Initialize();
            board.Initialize(GameType.Chess);

            // Subscribe to events
            board.MoveProposed += referee.OnMoveProposed;
            referee.GameContextChanged += board.OnGameContextChanged;
            referee.GameContextChanged += OnRefereeGameContextChanged;
            GameContext.StateChanged += () => SaveGame("current_game.json");
        }

        public override void Start()
        {
            // Ensure the layout is set before starting the referee
            referee.Start(chessLayout); // Ensure the Start method passes the layout
        }

        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            // Handle changes to game context from referee
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

            // JSON serialization settings
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Converters = new List<JsonConverter> { new ALayoutConverter(new ChessLayoutFactory()), new CoordinateConverter() }
            };

            // Serialize and save game context
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
            // Load game context from file
            try
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Converters = new List<JsonConverter>
                    {
                        new ALayoutConverter(new ChessLayoutFactory()),
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
