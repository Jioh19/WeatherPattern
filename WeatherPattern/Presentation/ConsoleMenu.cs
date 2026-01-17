using WeatherPattern.Event;
using WeatherPattern.Models;
using WeatherPattern.Readers;
using WeatherPattern.Utils;

namespace WeatherPattern.Presentation;

public static class ConsoleMenu
{
    public static async Task<(List<Weather> weather, List<NameBot> bots)> GetWeatherAndBotsAsync()
    {
        List<Weather> weather;
        List<NameBot> bots;
        
        while (true)
        {
            try
            {
                Console.WriteLine("Input weather file: (input.json - input.xml)");
                var weatherFile = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(weatherFile))
                {
                    continue;
                }
                weather = await TypeSelector.ReadAsync(PathParser.GetPath(weatherFile));
                Console.WriteLine($"Found {weather.Count} weather entries");
                if (weather.Count > 0)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        IReader<NameBot> botReader = new BotReader();
        while (true)
        {
            try
            {
                Console.WriteLine("Reading bot file bots.json");
                bots = await botReader.ReadAsync(PathParser.GetPath("bots.json"));
                Console.WriteLine($"Found {bots.Count} bots");
                if (bots.Count > 0)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return (weather, bots);
    }
}