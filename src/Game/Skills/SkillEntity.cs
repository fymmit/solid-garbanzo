using System.Numerics;
using Core;

public abstract class SkillEntity : Entity
{
    public abstract void Invoke(Vector2 initialPosition, Vector2 targetPosition);
}
