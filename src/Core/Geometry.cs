// using System.Numerics;
using Core;
using Raylib_cs;

public static class Geometry
{
    public static bool CheckCollision(Entity first, Entity second)
    {
        return Raylib.CheckCollisionCircles(first.Position, first.Radius, second.Position, second.Radius);
        // if (first.Shape == Shape.Circle)
        // {
        //     var collisionPoints = new Vector2[0];
        //     switch (second.Shape)
        //     {
        //         case Shape.Circle:
        //             return Raylib.CheckCollisionCircles(first.Position, first.Radius, second.Position, second.Radius);
        //         case Shape.Square:
        //             var rectangle = new Rectangle(new(second.Position.X - second.Radius, second.Position.Y - second.Radius), new(second.Radius * 2));
        //             return Raylib.CheckCollisionCircleRec(first.Position, first.Radius, rectangle);
        //     }
        // }
        //
        // if (first.Shape == Shape.Square)
        // {
        //     switch (second.Shape)
        //     {
        //         case Shape.Circle:
        //             var rectangle = new Rectangle(new(first.Position.X - first.Radius, first.Position.Y - first.Radius), new(first.Radius * 2));
        //             return Raylib.CheckCollisionCircleRec(second.Position, second.Radius, rectangle);
        //         case Shape.Square:
        //             var rectangle1 = new Rectangle(new(first.Position.X - first.Radius, first.Position.Y - first.Radius), new(first.Radius * 2));
        //             var rectangle2 = new Rectangle(new(second.Position.X - second.Radius, second.Position.Y - second.Radius), new(second.Radius * 2));
        //             return Raylib.CheckCollisionRecs(rectangle1, rectangle2);
        //     }
        // }
        //
        // return false;
    }

    // public static bool CheckCollision(Vector2 point, Entity obstacle)
    // {
    //     switch (obstacle.Shape)
    //     {
    //         case Shape.Circle:
    //             return Raylib.CheckCollisionPointCircle(point, obstacle.Position, obstacle.Radius);
    //         case Shape.Square:
    //             var rectangle = new Rectangle(new(obstacle.Position.X - obstacle.Radius, obstacle.Position.Y - obstacle.Radius), new(obstacle.Radius * 2));
    //             return Raylib.CheckCollisionPointRec(point, rectangle);
    //     }
    //
    //     return false;
    // }
}


public enum Shape
{
    Circle,
    Square
}
