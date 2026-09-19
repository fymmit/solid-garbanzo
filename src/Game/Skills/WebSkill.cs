using System.Numerics;
using Core;

public class WebSkill : SkillEntity
{
    private Cooldown _cooldown = new Cooldown(5f);

    public WebSkill()
    {
        Attach(_cooldown);
    }

    public override void Invoke(Vector2 initialPosition, Vector2 targetPosition)
    {
        if (_cooldown.IsReady())
        {
            _cooldown.Reset();
            EntityManager.Create<Web>(targetPosition);
        }
    }
}
