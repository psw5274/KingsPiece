public class EffectImmovable : Effect
{
    public EffectImmovable(Piece target, Skill.ApplyingTiming timing, int remainDuration)
        : base(target, timing, remainDuration)
    {
    }

    public override void Active()
    {
        target.movableCount = 0;
    }

    public override void Deactive()
    {
    }
}
