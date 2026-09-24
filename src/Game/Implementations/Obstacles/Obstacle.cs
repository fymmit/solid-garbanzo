using Core;

public class Obstacle : Entity
{
    public Obstacle()
    {
        Radius = 16f;
    }

    public override void Render()
    {
        Gfx.Renderer.DrawCircle(Position, Radius, 50, 180, 50, 255, true);
    }
}
