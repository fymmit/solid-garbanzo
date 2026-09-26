public static class Program
{
    [STAThread]
    public static void Main()
    {
        bool.TryParse(Environment.GetEnvironmentVariable("SIMULATION"), out var simulation);

        var game = new GameRuntime();

        game.Run(simulation);
    }
}
