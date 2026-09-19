using Core;

public class Web : Entity
{
    public Web()
    {
        Radius = 36f;
        Attach<WebBehaviour>();
        Attach(new Duration(5f));
    }
}

public class WebBehaviour : Behaviour
{
    public override void Update(float delta)
    {
        var enemies = EntityManager.Find<Enemy>();
        foreach (var enemy in enemies)
        {
            if (Geometry.CheckCollisionCircles(Parent.Position, Parent.Radius, enemy.Position, enemy.Radius))
            {
                enemy.GetComponent<MovementBehaviour>().MovementSpeedCoefficient = 0.5f;
            }
        }
    }
}
