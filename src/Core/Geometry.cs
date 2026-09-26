using System.Numerics;

public static class Geometry
{
    public static bool CheckCollisionCircles(Vector2 firstPosition, float firstRadius, Vector2 secondPosition, float secondRadius)
    {
        var radius = firstRadius + secondRadius;
        return Vector2.DistanceSquared(firstPosition, secondPosition) <= radius * radius;
    }
}
