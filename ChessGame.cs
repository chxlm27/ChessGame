﻿using Newtonsoft.Json;
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

            // Initialize GameContext if not already done
            if (GameContext == null)
            {
                GameContext = new Context();
                GameContext.Layout = new ChessLayout(); // Create and assign a new ChessLayout
                GameContext.Layout.Initialize(); // Initialize the layout with chess pieces
            }

            board.Initialize();
            referee.Initialize();

            board.MoveProposed += referee.OnMoveProposed;
            referee.GameContextChanged += board.OnGameContextChanged;

            // Set up game context and listen for state changes
            referee.GameContextChanged += OnRefereeGameContextChanged;

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

        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContext = e.NewContext;
            GameContextChanged?.Invoke(this, e);
        }

        public override void Start()
        {
           //Arbitrul anunta jocul, se lanseaza OnRefereeGameContextChanged
        }
    }
}
