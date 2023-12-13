using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace SerializationEdblock;

public class SerializationProject
{
    public static async void Write(List<BlockSymbol> blockSymbols, string pathFile)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        };

        using var fileStream = new FileStream(pathFile, FileMode.Create);
        await JsonSerializer.SerializeAsync(fileStream, blockSymbols, options);
    }

    public static async Task<List<BlockSymbol>> Read(string pathFile)
    {
        using var fileStream = new FileStream(pathFile, FileMode.Open);
        var blockSymbols = await JsonSerializer.DeserializeAsync<List<BlockSymbol>>(fileStream);

        if (blockSymbols == null)
        {
            throw new Exception("Ошибка при загрузи файла");
        }

        return blockSymbols;
    }
}