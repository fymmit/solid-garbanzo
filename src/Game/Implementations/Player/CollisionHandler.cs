using Core;

internal class CollisionHandler : Behaviour
{
    public override void Update(float delta)
    {
        var harmfuls = EntityManager.Find<Entity>().Where(entity => entity.GetComponent<IHarmful>() is not null);
        foreach (var other in harmfuls)
        {
            var harmful = other.GetComponent<IHarmful>();
            if (harmful is null) continue;
            if (Geometry.CheckCollision(Parent, other))
            {
                Parent.GetComponent<IDamageable>()?.TakeDamage(harmful.Damage);
            }
        }
    }
}
