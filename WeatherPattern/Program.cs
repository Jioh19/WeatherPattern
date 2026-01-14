// See https://aka.ms/new-console-template for more information

using WeatherPattern.Event;
using WeatherPattern.Models;
using WeatherPattern.Readers;
using WeatherPattern.Utils;

List<Weather> weather;

while (true)
{
    try
    {
        Console.WriteLine("Input weather file: (input.json - input.xml)");
        var choice = Console.ReadLine();

        if (choice is null) continue;
        Console.WriteLine($"Reading {choice}");
        weather = await TypeSelector.ReadAsync(PathParser.GetPath(choice));
            
        Console.WriteLine($"Found {weather.Count} weather entries");
        if (weather.Count > 0) break;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
        
var botReader = new BotReader();
var bots = await botReader.ReadAsync(PathParser.GetPath("bots.json"));
        
Console.WriteLine($"Found {bots.Count} bots");

var manager = new EventManager(weather, bots);
manager.Report();
