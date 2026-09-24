using System.Numerics;

namespace Core;

public readonly struct Input
{
    public readonly Vector2 Movement { get; init; }

    public readonly bool PausePressed { get; init; }
    public readonly bool TimescaleDecreasePressed { get; init; }
    public readonly bool TimescaleIncreasePressed { get; init; }
}
