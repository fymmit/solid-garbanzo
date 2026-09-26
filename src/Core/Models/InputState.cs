using System.Numerics;

namespace Core;

public readonly struct InputState
{
    public readonly Vector2 Movement { get; init; }

    public readonly bool DebugTogglePressed { get; init; }
    public readonly bool PausePressed { get; init; }
    public readonly bool TimescaleDecreasePressed { get; init; }
    public readonly bool TimescaleIncreasePressed { get; init; }

    public readonly Vector2 MouseScreenPosition { get; init; }
    public readonly Vector2 MouseWorldPosition { get; init; }
}
