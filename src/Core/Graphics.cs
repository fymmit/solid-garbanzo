using Core.Providers;

public static class Gfx
{
    public static IGraphicsProvider Renderer = new RaylibGraphicsProvider();
}
