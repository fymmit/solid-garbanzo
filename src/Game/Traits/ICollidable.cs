using System.Numerics;

namespace Game.GameObjects.Traits;

interface ICollidable
{
    Vector2 ColliderPosition { get; }
    float ColliderRadius { get; }
}

interface IHarmful : ICollidable
{
    int Damage { get; }
}
