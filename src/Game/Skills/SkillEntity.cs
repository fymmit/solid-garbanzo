using System.Numerics;
using Core;

public abstract class SkillEntity : Entity
{
    // TODO: move cooldown timer logic into SkillEntity instead of being duplicated across skills

    public abstract void Invoke(Vector2 initialPosition, Vector2 targetPosition);
}
