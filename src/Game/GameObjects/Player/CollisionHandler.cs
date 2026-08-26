using static Raylib_cs.Raylib;
using Game.Traits;

namespace Game.GameObjects.Player;

internal class CollisionHandler : Behaviour
{
    private float _hitboxRadius = 32f;

    public override void Update(float delta)
    {
        if (Parent is null) return;

        var harmfuls = GameObjectManager.GameObjects.Where(go => go.GetComponent<IHarmful>() is not null);
        foreach (var go in harmfuls)
        {
            var harmful = go.GetComponent<IHarmful>();
            if (harmful is null) continue;
            if (CheckCollisionCircles(Parent.Position, _hitboxRadius, harmful.ColliderPosition, harmful.ColliderRadius))
            {
                Parent.GetComponent<IDamageable>()?.TakeDamage(harmful.Damage);
            }
        }
    }
}

