// See https://aka.ms/new-console-template for more information

using WeatherPattern.Event;
using WeatherPattern.Utils;

try
{
    var weather = await TypeSelector.ReadAsync(PathParser.GetPath("input.json"));
    Console.WriteLine("JSON data:");
    weather.ForEach(Console.WriteLine);
    
    weather = await TypeSelector.ReadAsync(PathParser.GetPath("input.xml"));
    Console.WriteLine("\nXML data:");
    weather.ForEach(Console.WriteLine);
}
catch (Exception e)
{
    Console.WriteLine(e);
}
