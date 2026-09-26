namespace Core;

public static class Logger
{
    private static DateTime _gameStartTime = DateTime.Now;
    public static void Log(string message)
    {
        var timestamp = (DateTime.Now - _gameStartTime);
        Console.WriteLine($"[{timestamp}] - {message}");
    }
}
