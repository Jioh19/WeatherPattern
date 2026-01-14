using WeatherPattern.Models;

namespace WeatherPattern.Event;

public static class BotSelector
{
    public static List<NameBot> SelectBot(List<NameBot> bots)
    {
        return bots.Where(x => x.Bot.Enabled).ToList();
    }
}