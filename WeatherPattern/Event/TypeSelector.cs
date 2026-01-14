using WeatherPattern.Models;
using WeatherPattern.Readers;

namespace WeatherPattern.Event;

public static class TypeSelector
{
    public static async Task<Weather> ReadAsync(string file)
    {
        var type = file.Split('.').Last();
        IReader<Weather> reader = type switch
        {
            "json" => new JsonReader(),
            "xml" => new XmlReader(),
            _ => throw new InvalidDataException("Invalid data type")
        };

        var weather = await reader.ReadAsync(file);
        return weather;
    }
}