/// <summary>
/// Piece의 ATK를 변화시키는 효과
/// </summary>
public class ModifierATK : Modifier
{
    /// <summary>
    /// Piece의 ATK를 변화시키는 효과
    /// </summary>
    /// <param name="target">효과를 받는 Piece</param>
    /// <param name="delta">변화량</param>
    /// <param name="method">변화시키는 방식</param>
    /// <param name="duration">유효한 턴 수</param>
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