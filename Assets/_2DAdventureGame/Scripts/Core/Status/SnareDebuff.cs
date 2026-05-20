using UnityEngine;

public class SnareDebuff : StatusEffect
{
    public SnareDebuff(StatusEffectData data)
    {
        this.data = data;
    }

    protected override void OnMaxStackReached(StatusEffectManager target)
    {
        base.OnMaxStackReached(target);
        target.player.ToggleIsSnared();
    }

    public override void OnRemove(StatusEffectManager target)
    {
        base.OnRemove(target);
        target.player.ToggleIsSnared();
    }
}
