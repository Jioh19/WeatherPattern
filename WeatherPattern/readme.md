# WeatherPattern

A C# console application for processing weather data and bot actions from JSON or XML files.

## Features
- Reads weather data from user-specified JSON or XML files
- Reads bot data from a JSON file
- Event-driven bot activation based on weather conditions
- SOLID, maintainable, and modular codebase

## Getting Started

1. **Clone the repository:**
   ```
   git clone https://github.com/yourusername/WeatherPattern.git
   ```

2. **Build the project:**
   Open the solution in JetBrains Rider or run:
   ```
   dotnet build
   ```

3. **Run the application:**
   ```
   dotnet run
   ```

4. **Follow the prompts:**
   - Enter the weather file name (e.g., `input.json` or `input.xml`)
   - The app will read `bots.json` for bot data

## Project Structure

- `Event/` - Event management logic
- `Models/` - Data models for weather and bots
- `Readers/` - File readers for JSON and XML
- `Presentation/ConsoleMenu.cs` - Handles user interaction
- `Utils/` - Utility classes

## Extending

To add support for new file formats, implement the `IReader<T>` interface and update the selector logic if needed.
