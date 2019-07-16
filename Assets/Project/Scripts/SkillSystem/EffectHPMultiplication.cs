public class EffectHPMultiplication : Effect
{
    private int value;

    public EffectHPMultiplication(Piece target, Skill.ApplyingTiming timing, int remainDuration, int value)
        : base(target, timing, remainDuration)
    {
        this.value = value;
    }

    public override void Active()
    {
        target.MultiplicationHPDelta = value;
        target.UpdateStatus();
    }

    public override void Deactive()
    {
        target.MultiplicationHPDelta = -value;
        target.UpdateStatus();
    }
}
