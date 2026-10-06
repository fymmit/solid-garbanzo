using System.Numerics;

namespace UIElements;

public class Button
{
    public void Draw(
        Vector2 position,
        Vector2 size,
        (byte R, byte G, byte B, byte A) color,
        (byte R, byte G, byte B, byte A) hoverColor,
        Action onClick,
        Action? onHover
    )
    {
        var mousePos = Input.Current.MouseScreenPosition;
        var isHovering = (mousePos.X > position.X
            && mousePos.Y > position.Y
            && mousePos.X < position.X + size.X
            && mousePos.Y < position.Y + size.Y
        );

        var activeColor = isHovering ? hoverColor : color;

        Gfx.Provider.DrawRectangle(
            new(position.X, position.Y),
            new(size.X, size.Y),
            activeColor.R,
            activeColor.G,
            activeColor.B,
            activeColor.A,
            false);

        if (isHovering)
        {
            if (onHover is not null)
            {
                onHover();
            }
            if (Input.Current.MouseLeftClicked)
            {
                onClick();
            }
        }
    }
}
