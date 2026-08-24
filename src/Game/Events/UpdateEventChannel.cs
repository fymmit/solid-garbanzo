namespace Game.Events;

public static class UpdateEventChannel
{
    public static event Action? UpdateLoopStartedEvent;

    public static void InvokeUpdateLoopStartedEvent()
    {
        UpdateLoopStartedEvent?.Invoke();
    }

    public static event Action? UpdateLoopFinishedEvent;

    public static void InvokeUpdateLoopFinishedEvent()
    {
        UpdateLoopFinishedEvent?.Invoke();
    }
}

