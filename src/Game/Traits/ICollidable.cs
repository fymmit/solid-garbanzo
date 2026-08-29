using System.Numerics;

interface ICollidable
{
    Vector2 ColliderPosition { get; }
    float ColliderRadius { get; }
}

interface IHarmful : ICollidable
{
    int Damage { get; }
}
