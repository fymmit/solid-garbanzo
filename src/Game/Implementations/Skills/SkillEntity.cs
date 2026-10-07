using System.Numerics;
using Core;
using Events;

public abstract class SkillEntity : Entity
{
    public string Name = "Skill";

    public TimerInstance? CooldownTimer;
    protected float _cooldown = 0;
    private bool _isReady = true;

    protected float _range = 0f;

    public void Invoke(Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_isReady && (targetPosition - initialPosition).Length() < _range)
        {
            SkillEventChannel.InvokeSkillUsedEvent(this);
            _isReady = false;
            InvokeSkill(initialPosition, targetPosition);
            CooldownTimer = Timers.CreateTimer(_cooldown, () => _isReady = true);
        }
    }

    protected virtual void InvokeSkill(Vector2 initialPosition, Vector2 targetPosition) { }
}
