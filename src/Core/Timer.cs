namespace Core;

public static class Timers
{
    internal static Timer Timer = EntityManager.Create<Timer>();
    internal static List<TimerInstance> TimerInstances = [];

    public static TimerInstance CreateTimer(float duration, Action callback)
    {
        var timerInstance = new TimerInstance()
        {
            Duration = duration,
            Callback = callback
        };
        TimerInstances.Add(timerInstance);
        return timerInstance;
    }
}

public class Timer : Entity
{
    public Timer()
    {
        Attach<TimerBehaviour>();
    }
}

public class TimerBehaviour : Behaviour
{
    public override void Update(float delta)
    {
        List<TimerInstance> markedForRemoval = [];
        foreach (var timer in Timers.TimerInstances)
        {
            timer.Duration -= delta;
            if (timer.Duration <= 0)
            {
                timer.Callback();
                markedForRemoval.Add(timer);
            }
        }

        foreach (var timer in markedForRemoval)
        {
            Timers.TimerInstances.Remove(timer);
        }
    }
}

public record TimerInstance
{
    internal float Duration;
    internal Action Callback = null!;
}
