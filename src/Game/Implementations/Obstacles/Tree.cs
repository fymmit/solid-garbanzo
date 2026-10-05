public class Tree : Obstacle
{
    public Tree()
    {
        Radius = 16f;
        Shape = Shape.Circle;
    }

    public override void Render()
    {
        Gfx.Provider.DrawCircle(Position, Radius, 125, 50, 25, 255, true);
    }
}
