public class EffectHPDamage : Effect
{
    int value;

    public EffectHPDamage(Piece target, Skill.ApplyingTiming timing, int remainDuration, int value)
        : base(target, timing, remainDuration)
    {
        this.value = value;
    }

    public override void Active()
    {
        target.CurrentHP -= value;
    }

    public override void Deactive()
    {
    }
}
