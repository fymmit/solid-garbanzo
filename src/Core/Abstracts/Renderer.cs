namespace Core;

public abstract class Renderer
{
    public Entity Parent { get; set; } = null!;
    public abstract void Render();
}
