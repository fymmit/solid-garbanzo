using Core;

internal class CollisionHandler : Behaviour
{
    private float _hitboxRadius = 32f;

    public override void Update(float delta)
    {
        var harmfuls = EntityManager.Find<Entity>().Where(entity => entity.GetComponent<IHarmful>() is not null);
        foreach (var go in harmfuls)
        {
            var harmful = go.GetComponent<IHarmful>();
            if (harmful is null) continue;
            if (Geometry.CheckCollisionCircles(Parent.Position, _hitboxRadius, harmful.ColliderPosition, harmful.ColliderRadius))
            {
                Parent.GetComponent<IDamageable>()?.TakeDamage(harmful.Damage);
            }
        }
    }
}
