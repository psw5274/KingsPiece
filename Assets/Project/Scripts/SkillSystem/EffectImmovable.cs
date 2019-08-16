using UnityEngine;

public class EffectImmovable : Effect
{
    private int value;

    public EffectImmovable(Piece target, Skill.ApplyingTiming timing, int remainDuration, int value)
        : base(target, timing, remainDuration)
    {
        this.value = value;
    }

    public override void Active()
    {
        target.movableCount = Mathf.Clamp(target.movableCount - value, 0, int.MaxValue);
    }

    public override void Deactive()
    {
    }
}
