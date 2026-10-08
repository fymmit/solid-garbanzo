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
            OriginalDuration = duration,
            Callback = callback
        };
        TimerInstances.Add(timerInstance);
        return timerInstance;
    }

    public static TimerInstance CreateInterval(float interval, Action callback)
    {
        var timerInstance = new TimerInstance()
        {
            Duration = interval,
            OriginalDuration = interval,
            IsInterval = true,
            Callback = callback
        };
        TimerInstances.Add(timerInstance);
        timerInstance.Callback();
        return timerInstance;

    }

    public static void Remove(this TimerInstance? timer)
    {
        if (timer is not null)
        {
            TimerInstances.Remove(timer);
        }
    }
}

internal class Timer : Entity
{
    public Timer()
    {
        Attach<TimerBehaviour>();
    }
}

internal class TimerBehaviour : Behaviour
{
    public override void Update(float delta)
    {
        List<TimerInstance> markedForRemoval = [];
        foreach (var timer in Timers.TimerInstances.ToArray())
        {
            timer.Duration -= delta;
            if (timer.Duration <= 0)
            {
                timer.Callback();
                if (timer.IsInterval)
                {
                    timer.Duration = timer.OriginalDuration;
                }
                else
                {
                    markedForRemoval.Add(timer);
                }
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
    public float Duration { get; internal set; }
    public float OriginalDuration { get; internal set; }
    internal bool IsInterval;
    internal Action Callback = null!;
}
