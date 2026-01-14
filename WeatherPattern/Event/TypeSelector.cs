using WeatherPattern.Models;
using WeatherPattern.Readers;

namespace WeatherPattern.Event;

public static class TypeSelector
{
    private static IReader<Weather> SelectReader(string file)
    {
        var type = file.Split('.').Last();
        return type switch
        {
            "json" => new JsonReader(),
            "xml" => new XmlReader(),
            _ => throw new InvalidDataException("Invalid data type")
        };
    }

    public static async Task<List<Weather>> ReadAsync(string file)
    {
        var reader = SelectReader(file);
        var weather = await reader.ReadAsync(file);
        return weather;
    }
}