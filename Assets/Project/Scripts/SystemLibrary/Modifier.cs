public abstract class Modifier
{
    public enum ModifyMethod { Addtion, Multiplication };

    protected ModifyMethod method = ModifyMethod.Addtion;
    protected int duration = 0;

    public System.Action Apply { get; protected set; }
    public System.Action Unapply { get; protected set; }

    public int GetDuration()
    {
        return duration;
    }

    public void DecreaseDuration()
    {
        --duration;
    }
}
