public class EffectATKMultiplication : Effect
{
    private int value;

    public EffectATKMultiplication(Piece target, Skill.ApplyingTiming timing, int remainDuration, int value)
        : base(target, timing, remainDuration)
    {
        this.value = value;
    }

    public override void Active()
    {
        target.MultiplicationATKDelta = value;
    }

    public override void Deactive()
    {
        target.MultiplicationATKDelta = -value;
    }
}
