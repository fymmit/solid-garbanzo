using Core;

public class BloodParticle : Particle
{
    public BloodParticle()
    {
        Timer = Timers.CreateTimer(3f, () => Destroy());
    }

    public override void Render()
    {
        byte opacity = 255;
        var size = 32f;
        if (Timer is not null)
        {
            var percentage = Timer.Duration / Timer.OriginalDuration;
            opacity = (byte)(255 * percentage);
            size = (1 - percentage) * 16f + 16f;
        }

        Gfx.Renderer.DrawPoly(Position, 6, size, 0, 255, 0, 0, opacity, false);
    }
}
