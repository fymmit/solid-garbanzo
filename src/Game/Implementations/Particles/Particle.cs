using Core;

public class Particle : Entity
{
    public TimerInstance? Timer { get; protected set; }
    public Particle()
    {
        DrawPriority = 1;
    }
}
