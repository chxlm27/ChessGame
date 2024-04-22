using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Chess
{
    [Serializable]
    public class ChessGame : AGame
    {
        [NonSerialized]
        private Board board;
        [NonSerialized]
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
            // Create a new SaveFileDialog instance
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set initial directory and default file name (optional)
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "saved_game.game";

            // Set file filter (optional)
            saveFileDialog.Filter = "Game Files (*.game)|*.game|All Files (*.*)|*.*";

            // Show the SaveFileDialog and check if the user clicked the OK button
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected file path from the dialog
                    string saveFilePath = saveFileDialog.FileName;

                    // Serialize and save the game to the selected file path
                    using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Create))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(fileStream, this);
                        Console.WriteLine("Game saved successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving game: {ex.Message}");
                }
            }
        }


        public override void Load()
        {
            // Create a new OpenFileDialog instance
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set initial directory and default file name filter (optional)
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Game Files (*.game)|*.game|All Files (*.*)|*.*";

            // Show the OpenFileDialog and check if the user clicked the OK button
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected file path from the dialog
                    string saveFilePath = openFileDialog.FileName;

                    // Load the game from the selected file path
                    using (FileStream fileStream = new FileStream(saveFilePath, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        ChessGame loadedGame = (ChessGame)formatter.Deserialize(fileStream);

                        // Assign loaded game data to current instance
                        this.board = loadedGame.board;
                        this.referee = loadedGame.referee;

                        // Reinitialize event handlers
                        this.board.MoveProposed -= this.referee.OnMoveProposed;
                        this.referee.GameContextChanged -= this.board.OnGameContextChanged;

                        this.board.MoveProposed += this.referee.OnMoveProposed;
                        this.referee.GameContextChanged += this.board.OnGameContextChanged;

                        Console.WriteLine("Game loaded successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading game: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("No game loaded.");
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
