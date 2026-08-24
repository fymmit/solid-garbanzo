using Game.GameObjects;

namespace Game.Traits;

public interface IComposable
{
    GameObject Parent { get; }
}
