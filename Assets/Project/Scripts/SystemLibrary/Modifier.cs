/// <summary>
/// 버프/디버프 효과를 적용시키는 추상 클래스
/// </summary>
public abstract class Modifier
{
    public enum ModifyMethod { Addtion, Multiplication };

    protected ModifyMethod method = ModifyMethod.Addtion;
    protected int duration = 0;

    public System.Action Apply { get; protected set; }
    public System.Action Unapply { get; protected set; }

    /// <summary>
    /// 남은 유효 턴 수 확인
    /// </summary>
    /// <returns>남은 유효 턴 수</returns>
    public int GetDuration()
    {
        return duration;
    }

    /// <summary>
    /// 유효 턴 수 하나 감소
    /// </summary>
    public void DecreaseDuration()
    {
        --duration;
    }
}
