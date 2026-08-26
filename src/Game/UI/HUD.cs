using Raylib_cs;
using Game.Traits;
using Game.Events;

namespace Game.UI;

public class HUD : IRenderable, IUpdatable
{
    private int _healthBarPercentage = 100;

    public HUD()
    {
        DamageEventChannel.DamageEvent += OnDamage;
    }

    public void Render()
    {
        var position = Raylib.GetScreenToWorld2D(new System.Numerics.Vector2(10, 10), Program.Camera);
        Raylib.DrawRectangle((int)position.X, (int)position.Y, 160, 40, Color.Black);
        Raylib.DrawText("HUD", (int)position.X + 4, (int)position.Y + 4, 16, Color.White);
    }

    public void Update(float delta) { }

    private void OnDamage(IDamageable target, int amount)
    {
        // FIX: this doesn't work because IDamageable is not IComposable so it doesn't have access to the parent element
        // the component system needs to be resigned

        // var player = target.Parent;
    }
}
