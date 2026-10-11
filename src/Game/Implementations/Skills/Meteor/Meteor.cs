using Core;

public class Meteor : Entity
{
    public Meteor()
    {
        DrawPriority = 2;
        Radius = 36f;
        Attach<MeteorBehaviour>();
    }

    public override void Render()
    {
        Gfx.Provider.DrawCircle(Position, Radius, 150, 50, 50, 200, false);
    }
}

public class MeteorBehaviour : Behaviour
{
    public override void Ready()
    {
        var enemies = EntityManager.Find<Enemy>();
        foreach (var enemy in enemies)
        {
            var damageable = enemy.GetComponent<IDamageable>();
            if (damageable is not null)
            {
                if (Geometry.CheckCollision(Parent, enemy))
                {
                    damageable.TakeDamage(20);
                }
            }
        }
    }

    public override void Update(float delta)
    {
    }
}
