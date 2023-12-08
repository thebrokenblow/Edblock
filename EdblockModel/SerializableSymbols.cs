using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.LineSymbols;

namespace EdblockModel;

[Serializable]
public class SerializableSymbols
{
    public List<BlockSymbolModel> BlocksSymbolModel { get; set; } 
    public List<DrawnLineSymbolModel> LinesSymbolModel { get; set; }

    public SerializableSymbols()
    {
        BlocksSymbolModel = new();
        LinesSymbolModel = new();
    }

    public async void SaveProject(string pathFile = "")
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        };

        using (FileStream fs = new FileStream("user.json", FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, BlocksSymbolModel, options);
        }

    }

    public void UploadProject(string pathFile = "")
    {
        using (FileStream fs = new FileStream("user.json", FileMode.Open))
        {
            List<BlockSymbolModel>? person = JsonSerializer.Deserialize<List<BlockSymbolModel>>(fs);
        }
    }
}