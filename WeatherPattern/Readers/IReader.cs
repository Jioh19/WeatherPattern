namespace WeatherPattern.Readers;

public interface IReader<T>
{
    Task<List<T>> ReadAsync(string filePath);
}