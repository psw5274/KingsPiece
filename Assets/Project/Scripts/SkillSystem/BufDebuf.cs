public class BufDebuf
{
    public BufDebufTag tag;
    public DurationType durationType;
    public int duration;
}

public enum BufDebufTag
{
    None,
    Buf,
    Debuf,
    Heal,
    Damage
}

public enum DurationType
{
    Turn,
    Use
}
