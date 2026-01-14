// See https://aka.ms/new-console-template for more information

using WeatherPattern.Event;
using WeatherPattern.Models;
using WeatherPattern.Readers;
using WeatherPattern.Utils;

try
{
    var weather = await TypeSelector.ReadAsync(PathParser.GetPath("input.json"));
    Console.WriteLine("JSON data:");
    weather.ForEach(Console.WriteLine);
    
    weather = await TypeSelector.ReadAsync(PathParser.GetPath("input.xml"));
    Console.WriteLine("\nXML data:");
    weather.ForEach(Console.WriteLine);
    
    var botReader = new BotReader();
    var bots = await botReader.ReadAsync(PathParser.GetPath("bots.json"));

    Console.WriteLine("\nBots:");
    foreach (var entry in BotSelector.SelectBot(bots))
    {
        Console.WriteLine($"Name: {entry.Name}");
        Console.WriteLine($"Bot: {entry.Bot}");
    }
    
    var eventManager = new EventManager(weather, bots);
    eventManager.Report();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
