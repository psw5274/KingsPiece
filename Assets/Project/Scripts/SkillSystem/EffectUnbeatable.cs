public class EffectUnbeatable : Effect
{
    public EffectUnbeatable(Piece target, Skill.ApplyingTiming timing, int remainDuration)
        : base(target, timing, remainDuration)
    {
    }

    public override void Active()
    {
        ++target.unbeatableCount;
    }

    public override void Deactive()
    {
        --target.unbeatableCount;
    }
}
