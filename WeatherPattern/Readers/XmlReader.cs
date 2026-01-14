using WeatherPattern.Models;

namespace WeatherPattern.Readers;

public class XmlReader : IReader<Weather>
{
    public Task<List<Weather>> ReadAsync(string filePath)
    {
        throw new NotImplementedException();
    }
}