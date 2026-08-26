using Raylib_cs;

namespace Game.GameObjects.Player;

public class Player : GameObject
{
    public Player()
    {
        Components = [
            Attach<PlayerController>(),
            Attach<CollisionHandler>(),
            Attach<PlayerDamageHandler>()
        ];
        Renderer = new Renderer(this, Color.Blue, 32f, Shape.Circle);
    }
}

