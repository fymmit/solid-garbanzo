using System.Numerics;

public readonly record struct GameInput(Vector2 Movement, Vector2 AimPosition, bool FirePressed);

internal static class CurrentInput
{
    public static GameInput Value { get; set; }
}
