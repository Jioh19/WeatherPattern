using WeatherPattern.Models;

namespace WeatherPattern.Event;

public class EventManager(List<Weather> weathers, List<NameBot> bots)
{
    private readonly List<Weather> _weathers = weathers ?? throw new ArgumentNullException(nameof(weathers));
    private List<NameBot> _bots = bots ?? [];

    private void UpdateBots(List<NameBot> bots)
    {
        this._bots = bots.Where(x => x.Bot.Enabled).ToList();
    }

    public void Report()
    {
        UpdateBots(_bots);
        _weathers.ForEach(w =>
        {
            Console.WriteLine($"\nChecking {w.Location}");
            foreach (var entry in _bots)
            {
                var name = entry.Name;
                var bot = entry.Bot;

                switch (bot.Type)
                {
                    case BotTypeEnum.Humidity:
                        if (w.Humidity > bot.Value)
                        {
                            Console.WriteLine($"{name} activated!");
                            Console.WriteLine($"{name}: {bot.Message}");
                        }

                        break;
                    case BotTypeEnum.Temperature:
                        if (bot.Value <= 0)
                        {
                            if (w.Temperature < bot.Value)
                            {
                                Console.WriteLine($"{name} activated!");
                                Console.WriteLine($"{name}: {bot.Message}");
                            }
                        }
                        else if (w.Temperature > bot.Value)
                        {
                            Console.WriteLine($"{name} activated!");
                            Console.WriteLine($"{name}: {bot.Message}");
                        }

                        break;
                    default:
                        break;
                }
            }
        });
    }
}