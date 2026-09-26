using Core;

namespace Events;

public static class SkillEventChannel
{
    public static event Action<SkillEntity>? SkillUsedEvent;

    public static void InvokeSkillUsedEvent(SkillEntity skill)
    {
        Logger.Log($"{skill} used");
        SkillUsedEvent?.Invoke(skill);
    }
}

