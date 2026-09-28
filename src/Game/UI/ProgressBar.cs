using System.Numerics;

namespace UIElements;

public class ProgressBar
{
    public void Draw(
        Vector2 position,
        Vector2 size,
        float percentage,
        (byte R, byte G, byte B, byte A) Fg,
        (byte R, byte G, byte B, byte A) Bg,
        int padding
    )
    {
        Gfx.Provider.DrawRectangle(
            new(position.X, position.Y),
            new(size.X, size.Y),
            Bg.R,
            Bg.G,
            Bg.B,
            Bg.A,
            false);

        Gfx.Provider.DrawRectangle(
            new(position.X + padding, position.Y + padding),
            new(size.X * percentage - 2 * padding, size.Y - 2 * padding),
            Fg.R,
            Fg.G,
            Fg.B,
            Fg.A,
            false);
    }
}
