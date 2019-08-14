public class EffectATKAddition : Effect
{
    private int value;

    public EffectATKAddition(Piece target, Skill.ApplyingTiming timing, int remainDuration, int value)
        : base(target, timing, remainDuration)
    {
        this.value = value;
    }

    public override void Active()
    {
        target.AdditionATKDelta = value;
    }

    public override void Deactive()
    {
        target.AdditionATKDelta = -value;
    }
}
