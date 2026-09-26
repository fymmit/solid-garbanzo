using System.Numerics;
using Core;
using Events;

public abstract class SkillEntity : Entity
{
    public TimerInstance? CooldownTimer;
    public string Name = "Skill";
    protected float _cooldown = 0;
    private bool _isReady = true;

    public void Invoke(Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_isReady)
        {
            SkillEventChannel.InvokeSkillUsedEvent(this);
            _isReady = false;
            InvokeSkill(initialPosition, targetPosition);
            CooldownTimer = Timers.CreateTimer(_cooldown, () => _isReady = true);
        }
    }

    protected virtual void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition) { }
}
