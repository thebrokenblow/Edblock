using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace SerializationEdblock;

public class SerializationProject
{
    public static async void Write(ProjectSerializable projectSerializable, string pathFile)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        };

        using var fileStream = new FileStream(pathFile, FileMode.Create);
        await JsonSerializer.SerializeAsync(fileStream, projectSerializable, options);
    }

    public static async Task<ProjectSerializable> Read(string pathFile)
    {
        using var fileStream = new FileStream(pathFile, FileMode.Open);
        var projectSerializable = await JsonSerializer.DeserializeAsync<ProjectSerializable>(fileStream);

        if (projectSerializable == null)
        {
            throw new Exception("Ошибка при загрузи файла");
        }

        return projectSerializable;
    }
}