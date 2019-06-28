public class ModifierATK : Modifier
{
    public ModifierATK(Piece target, int delta, ModifyMethod method, int duration)
    {
        this.duration = duration;

        switch (method)
        {
            case ModifyMethod.Addtion:
                Apply = () => target.statATK += delta;
                Unapply = () => target.statATK -= delta;
                break;
            case ModifyMethod.Multiplication:
                Apply = () => target.statATK *= delta;
                Unapply = () => target.statATK /= delta;
                break;
            default:
                break;
        }
    }
}