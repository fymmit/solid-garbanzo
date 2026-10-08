using System.Numerics;
using Core;
using Events;

public abstract class SkillEntity : Entity
{
    public string Name { get; protected set; } = "Skill";
    public float Cooldown { get; protected set; } = 0;
    public float CastTime { get; protected set; } = 0f;
    public float Range { get; protected set; } = 0f;

    public TimerInstance? CooldownTimer;
    private bool _isReady = true;

    public void Invoke(Vector2 casterPosition, Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_isReady && (targetPosition - casterPosition).Length() < Range)
        {
            SkillEventChannel.InvokeSkillUsedEvent(this);
            _isReady = false;
            if (CastTime > 0)
            {
                Timers.CreateTimer(CastTime, () => InvokeWithCooldown(initialPosition, targetPosition));
            }
            else
            {
                InvokeWithCooldown(initialPosition, targetPosition);
            }
        }
    }

    protected virtual void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition) { }

    private void InvokeWithCooldown(Vector2 initialPosition, Vector2 targetPosition)
    {
        InvokeSkill(initialPosition, targetPosition);
        CooldownTimer = Timers.CreateTimer(Cooldown, () => _isReady = true);
    }
}
