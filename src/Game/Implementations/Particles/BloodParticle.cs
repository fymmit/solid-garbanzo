using Core;

public class BloodParticle : Entity
{
    public BloodParticle()
    {
        DrawPriority = 1;
        Timers.CreateTimer(3f, () => Destroy());
    }
}
