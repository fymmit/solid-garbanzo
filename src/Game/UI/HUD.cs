using Raylib_cs;
using Game.Traits;
using Game.Events;
using Game.GameObjects;
using Game.GameObjects.Player;

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
        // TODO: fix all these numbers
        Raylib.DrawRectangle(10, 10, 160, 40, Color.Black);
        Raylib.DrawText("HUD", 10 + 4, 10 + 4, 16, Color.White);

        // some sort of health bar
        Raylib.DrawRectangle(10 - 2, 80 - 2, 104, 44, Color.Black);
        Raylib.DrawRectangle(10, 80, _healthBarPercentage, 40, Color.Red);
    }

    public void Update(float delta) { }

    private void OnDamage(GameObject? target, int amount)
    {
        if (target is Player)
        {
            _healthBarPercentage -= amount;
        }
    }
}
