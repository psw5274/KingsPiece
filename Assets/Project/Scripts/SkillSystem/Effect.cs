public abstract class Effect
{
    public Piece target;
    public Skill.ApplyingTiming timing;
    public int remainDuration;

    protected Effect(Piece target, Skill.ApplyingTiming timing, int remainDuration)
    {
        this.target = target;
        this.timing = timing;
        this.remainDuration = remainDuration;
    }

    public abstract void Active();
    public abstract void Deactive();
}
