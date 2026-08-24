using Raylib_cs;

namespace Game.GameObjects.Player;

public class Player : GameObject
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

    public override void Ready()
    {
        Console.WriteLine("Player ready.");
    }
}

