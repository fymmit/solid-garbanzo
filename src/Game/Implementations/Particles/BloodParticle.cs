using Core;

public class BloodParticle : Particle
{
    public BloodParticle()
    {
        Timer = Timers.CreateTimer(3f, () => Destroy());
    }
}
