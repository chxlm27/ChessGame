﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

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
                GameContext = new Context();  // Properly instantiate if not already done
            }

            board.Initialize();
            referee.Initialize();

            board.MoveProposed += referee.OnMoveProposed;
            referee.GameContextChanged += board.OnGameContextChanged;

            // Set up game context and listen for state changes

            referee.GameContextChanged += (sender, args) =>
            {
                GameContext = args.NewContext;
                // Optionally add logic to handle null or invalid contexts
                if (GameContext == null)
                {
                    GameContext = new Context(); // Re-initialize if null
                }
            };
            GameContext.StateChanged += () => SaveGame("current_game.json");
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
                File.WriteAllText(filePath, json);  // Write directly to the path chosen in the dialog
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


        public override void Start()
        {//arbitru anunta jocul, se lanseaza ONRefereeGameContextChanged
        }

        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContext = e.NewContext; // Update local context
            GameContextChanged?.Invoke(this, e);
        }

    }
}
