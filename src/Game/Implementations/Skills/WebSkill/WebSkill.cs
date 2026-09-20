using System.Numerics;
using Core;

public class WebSkill : SkillEntity
{
    private const float COOLDOWN = 5f;
    private bool _isReady = true;

    public override void Invoke(Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_isReady)
        {
            _isReady = false;
            var web = EntityManager.Create<SpiderWeb>(targetPosition);
            Timers.CreateTimer(3f, () => web.Destroy());
            Timers.CreateTimer(COOLDOWN, () => _isReady = true);
        }
    }
}
