using static Raylib_cs.Raylib;
using Game.Traits;

namespace Game.GameObjects.Player;

internal class CollisionHandler(GameObject parent) : IComposable, IUpdatable
{
    public GameObject Parent => parent;

    private float _hitboxRadius;

    public void Update(float delta)
    {
        var harmfuls = Program.GameObjectManager.GameObjects.Where(go => go is IHarmful);
        foreach (var go in harmfuls)
        {
            var harmful = (IHarmful)go;
            if (CheckCollisionCircles(Parent.Position, _hitboxRadius, harmful.ColliderPosition, harmful.ColliderRadius))
            {
                Parent.GetComponent<IDamageable>()?.TakeDamage(harmful.Damage);
            }
        }
    }
}


