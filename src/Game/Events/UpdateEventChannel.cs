namespace Game.Events;

public static class UpdateEventChannel
{
    public static event Action? UpdateLoopStartedEvent;

    public static void InvokeUpdateLoopStartedEvent()
    {
        UpdateLoopStartedEvent?.Invoke();
    }

    public static event Action<float>? UpdateEvent;

    public static void InvokeUpdateEvent(float delta)
    {
        UpdateEvent?.Invoke(delta);
    }

    public static event Action? UpdateLoopFinishedEvent;

    public static void InvokeUpdateLoopFinishedEvent()
    {
        UpdateLoopFinishedEvent?.Invoke();
    }
}

