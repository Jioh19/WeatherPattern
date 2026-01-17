using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherPattern.Models;

namespace WeatherPattern.Readers;

public class BotReader : IReader<NameBot>
{
    public async Task<List<NameBot>> ReadAsync(string filePath)
    {
        var jsonString = await File.ReadAllTextAsync(filePath);
        try
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(jsonString);
            var bots = new List<NameBot>();
            if (dict is null)
            {
                return bots;
            }
            foreach (var (key, data) in dict)
            {
                var nameBot = new NameBot();
                nameBot.Name = key;
                var bot = new Bot();
                bot.Enabled = data["enabled"]?.Value<bool>() ?? false;
                bot.Message = data["message"]?.Value<string>() ?? string.Empty;
                if (data["humidityThreshold"] is not null)
                {
                    bot.Type = BotTypeEnum.Humidity;
                    bot.Value = data["humidityThreshold"]?.Value<int>() ?? 0;
                }
                else if (data["temperatureThreshold"] is not null)
                {
                    bot.Type = BotTypeEnum.Temperature;
                    bot.Value = data["temperatureThreshold"]?.Value<int>() ?? 0;
                }
                nameBot.Bot = bot;
                bots.Add(nameBot);
            }
            return bots;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error parsing Json: {e.Message}");
            throw;
        }
    }
}