using System.Numerics;
using Core;
using Events;

public class AutoTargeting : Behaviour
{
    private Enemy? _target;
    private Skillbar? _skillbar;

    public override void Ready()
    {
        DeathEventChannel.DeathEvent += OnDeathEvent;
        _skillbar = Parent.GetComponent<Skillbar>();
    }

    public override void Update(float delta)
    {
        if (_target is null)
        {
            var closestEnemy = EntityManager
                .Find<Enemy>()
                .OrderBy(e => Vector2.Distance(Parent.Position, e.Position))
                .FirstOrDefault();

            _target = closestEnemy;
        }

        if (_skillbar is not null && _target is not null)
        {
            foreach (var skill in _skillbar.Skills)
            {
                skill.Invoke(Parent.Position, _target.Position);
            }
        }
    }

    public override void DebugRender()
    {
        if (_target is not null)
        {
            Gfx.Renderer.DrawLine(Parent.Position, _target.Position, 255, 255, 0, 255);
        }
    }

    void OnDeathEvent(Entity? target)
    {
        if (target == _target)
        {
            _target = null;
        }
    }

    public override void OnDestroy()
    {
        DeathEventChannel.DeathEvent -= OnDeathEvent;
    }
}
