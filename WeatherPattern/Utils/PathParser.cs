namespace WeatherPattern.Utils;

public static class PathParser
{
    public static string GetPath(string fileName)
    {
        var directPath = Path.Combine(AppContext.BaseDirectory, "Inputs", fileName);
        var consolePath = Path.Combine(Directory.GetCurrentDirectory(), "Inputs", fileName);
        var riderPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName ?? "", "WeatherPattern", "Inputs", fileName);

        if (File.Exists(directPath))
        {
            return directPath;
        }

        if (File.Exists(consolePath))
        {
            return consolePath;
        }

        if (File.Exists(riderPath))
        {
            return riderPath;
        }
    
        throw new FileNotFoundException($"File not found in either expected location: {directPath}, {consolePath} nor {riderPath}");
    }
}