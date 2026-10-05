using Core;

public class SpiderWeb : Entity
{
    public SpiderWeb()
    {
        Radius = 36f;
        Attach<WebBehaviour>();
    }

    public override void Render()
    {
        Gfx.Provider.DrawCircle(Position, Radius, 150, 150, 150, 255, false);
    }
}

public class WebBehaviour : Behaviour
{
    private const float EFFECT_DURATION = 3f;
    private const float EFFECT_STRENGTH = .5f;

    public override void Update(float delta)
    {
        var enemies = EntityManager.Find<Enemy>();
        foreach (var enemy in enemies)
        {
            var movementBehaviour = enemy.GetComponent<MovementBehaviour>();
            if (movementBehaviour is not null)
            {
                if (Geometry.CheckCollision(Parent, enemy))
                {
                    if (movementBehaviour.MovementSpeedCoefficient >= 1f)
                    {
                        movementBehaviour.MovementSpeedCoefficient -= EFFECT_STRENGTH;
                        Timers.CreateTimer(EFFECT_DURATION, () => movementBehaviour.MovementSpeedCoefficient += EFFECT_STRENGTH);
                    }
                }
            }
        }
    }
}
