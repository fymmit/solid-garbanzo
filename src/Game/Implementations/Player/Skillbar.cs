using Core;

internal class Skillbar : Behaviour
{
    public List<SkillEntity> Skills = [EntityManager.Create<RangedAttack>()];
}
