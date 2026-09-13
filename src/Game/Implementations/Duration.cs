using Core;

public class Duration : Behaviour
{
    private float _timeUntilDestroy;

    public float OriginalLifeTime;
    public float RemainingLifeTime => _timeUntilDestroy;

    public Duration(float lifeTimeInSeconds)
    {
        OriginalLifeTime = lifeTimeInSeconds;
        _timeUntilDestroy = lifeTimeInSeconds;
    }

    public override void Update(float delta)
    {
        _timeUntilDestroy -= delta;
        if (_timeUntilDestroy <= 0)
        {
            Parent.Destroy();
        }
    }
}
