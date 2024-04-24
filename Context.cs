using Chess;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

public class Context
{
    public PieceColors CurrentPlayer { get; set; }
    public ALayout Layout { get; set; }

    public Context()
    {
        CurrentPlayer = PieceColors.White;
        Layout = new ChessLayout();
        Layout.Initialize();
    }

    public Context Clone()
    {
        return new Context
        {
            CurrentPlayer = this.CurrentPlayer,
            Layout = this.Layout.Clone()
        };
    }

    public void SwitchPlayer()
    {
        CurrentPlayer = (CurrentPlayer == PieceColors.White) ? PieceColors.Black : PieceColors.White;
    }

    public void Move(Coordinate originalCell, Coordinate destinationCell)
    {
        if (Layout.ContainsKey(originalCell))
        {
            APiece piece = Layout[originalCell];
            if (piece != null)
            {
                Layout.Remove(originalCell);

                if (Layout.ContainsKey(destinationCell))
                {
                    Layout.Remove(destinationCell);
                }

                Layout.Add(destinationCell, piece);

                // Save the game state after each move
                Save("current_game.json");
            }
        }
    }

    public void Save(string fileName)
    {
        string filePath = Path.Combine(@"F:\IT Perspectives\", fileName);
        var settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = new List<JsonConverter> { new ALayoutConverter(), new CoordinateConverter() }
        };
        string json = JsonConvert.SerializeObject(this, settings);
        try
        {
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to save game: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
