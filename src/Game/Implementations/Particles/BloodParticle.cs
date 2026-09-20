using Core;

public class BloodParticle : Entity
{
    public BloodParticle()
    {
        Timers.CreateTimer(3f, () => Destroy());
    }
}
