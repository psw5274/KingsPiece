public class EffectHPAddition : Effect
{
    private int value;

    public EffectHPAddition(Piece target, Skill.ApplyingTiming timing, int remainDuration, int value)
        : base(target, timing, remainDuration)
    {
        this.value = value;
    }

    public override void Active()
    {
        target.AdditionHPDelta = value;
        target.UpdateStatus();
    }

    public override void Deactive()
    {
        target.AdditionHPDelta = -value;
        target.UpdateStatus();
    }
}
