using Raylib_cs;

namespace Game.GameObjects.Player;

internal class Player : GameObject
{
    public Player()
    {
        Components = [
            new PlayerController(this),
            new CollisionHandler(this),
            new Renderer(this, Color.Blue, 32f, Shape.Circle),
            new PlayerDamageHandler(this)
        ];
    }

    internal override void Ready()
    {
        Console.WriteLine("Player ready.");
    }
}

