using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.LineSymbols;
using System.Text.Json;

namespace EdblockModel;

public class SerializableSymbols
{
    public List<BlockSymbolModel> BlocksSymbolModel { get; init; } = new();
    public List<DrawnLineSymbolModel> LinesSymbolModel { get; init; } = new();

    public async void SaveProject(string pathFile = "")
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        using FileStream fs = new FileStream("C:\\Users\\Artem\\Desktop\\user.json", FileMode.Create);
        await JsonSerializer.SerializeAsync(fs, this, options);
    }
}