using Core;

public class Skillbar : Behaviour
{
    public List<SkillEntity> Skills = [
        EntityManager.Create<RangedAttack>(),
        EntityManager.Create<WebSkill>()
    ];
}
