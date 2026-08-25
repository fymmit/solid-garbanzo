using Game.Traits;
using Game.Events;

namespace Game.GameObjects.Player;

internal class PlayerDamageHandler(GameObject parent) : IComposable, IDamageable
{
    public GameObject Parent => parent;

    public void TakeDamage(int damage)
    {
        DamageEventChannel.InvokeDamageEvent(damage);
        GameObjectManager.Remove(Parent);
    }
}


