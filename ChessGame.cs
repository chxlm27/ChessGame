using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;

namespace Chess
{
    public class ChessGame : AGame
    {
        private Board board;
        private Referee referee;
        public Context GameContext { get; private set; }

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public override void Initialize(Board _board)
        {
            board = _board;
            referee = new Referee();

            if (GameContext == null)
            {
                GameContext = new Context();
                GameContext.Layout = new ChessLayout();
                GameContext.Layout.Initialize();
            }

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
            // Set the initial player to White
            GameContext.CurrentPlayer = PieceColors.Black;
            // Force a context change event to start the game with the initial setup
            GameContextChangedEventArgs args = new GameContextChangedEventArgs(GameContext.Clone());
            OnRefereeGameContextChanged(this, args);
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
                Converters = new List<JsonConverter> { new ALayoutConverter(), new CoordinateConverter() }
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
                    Converters = new List<JsonConverter> { new ALayoutConverter(), new CoordinateConverter() }
                };
                string json = File.ReadAllText(filePath);
                Context loadedContext = JsonConvert.DeserializeObject<Context>(json, settings);
                GameContextChanged?.Invoke(this, new GameContextChangedEventArgs(loadedContext));
                return loadedContext;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                return null;
            }
        }
    }
}
