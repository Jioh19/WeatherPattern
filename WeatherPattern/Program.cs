// See https://aka.ms/new-console-template for more information

using WeatherPattern.Event;
using WeatherPattern.Presentation;

var (weather, bots) = await ConsoleMenu.GetWeatherAndBotsAsync();

var manager = new EventManager(weather, bots);
manager.Report();
