using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chess
{
    [Serializable]
    public class ChessGame : AGame
    {
        private Board board;
        private Referee referee;

        public event EventHandler<GameContextChangedEventArgs> GameContextChanged;

        public ChessGame()
        {
        }

        public override void Initialize(Board _board)
        {
            board = _board;
            referee = new Referee();

            board.Initialize();
            referee.Initialize();

            board.MoveProposed += referee.OnMoveProposed;
            referee.GameContextChanged += board.OnGameContextChanged;

        }


        public override void Save()
        {
            string saveFilePath = GetSaveFilePath();
            using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, this);
            }
        }

        public override void Load()
        {
            string saveFilePath = GetSaveFilePath();
            if (File.Exists(saveFilePath))
            {
                using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    ChessGame loadedGame = (ChessGame)formatter.Deserialize(fileStream);
                }
            }
            else
            {
                Console.WriteLine("No saved game found.");
            }
        }

        private string GetSaveFilePath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(currentDirectory, "saved_game.game");
        }


        public override void Start()
        {
        }

        private void OnRefereeGameContextChanged(object sender, GameContextChangedEventArgs e)
        {
            GameContextChanged?.Invoke(this, e);
        }
    }
}
