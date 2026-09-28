using System.Numerics;
using UIElements;

public static class UI
{
    private readonly static ProgressBar _progressBar = new ProgressBar();
    public static void ProgressBar(
        Vector2 position,
        Vector2 size,
        float percentage,
        (byte R, byte G, byte B, byte A) Fg,
        (byte R, byte G, byte B, byte A) Bg,
        int padding
    ) => _progressBar.Draw(position, size, percentage, Fg, Bg, padding);
}
