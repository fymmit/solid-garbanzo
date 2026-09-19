using Core;

public class Cooldown : Behaviour
{
    private float _cooldownValue;
    private float _remainingCooldown;
    public float RemainingCooldown => Math.Max(_remainingCooldown, 0);

    public Cooldown(float cooldown)
    {
        _cooldownValue = cooldown;
        _remainingCooldown = cooldown;
    }

    public override void Update(float delta)
    {
        _remainingCooldown -= delta;
    }

    public void Reset()
    {
        _remainingCooldown = _cooldownValue;
    }

    public bool IsReady() => _remainingCooldown <= 0;
}
