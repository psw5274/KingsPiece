using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 버프/디버프 효과를 관리하는 큐
/// </summary>
public class ModifierQueue
{
    private List<Modifier> modifiers = new List<Modifier>();

    /// <summary>
    /// 버프/디버프 변화 적용
    /// </summary>
    /// <param name="modifier">버프/디버프 효과</param>
    public void Activate(Modifier modifier)
    {
        modifier.Apply();
        modifiers.Add(modifier);
    }

    /// <summary>
    /// 남은 턴 수가 0인 효과 제거
    /// </summary>
    private void RemoveExpired()
    {
        var expireds = modifiers.Where(modifier => modifier.GetDuration() == 0);
        foreach (var expired in expireds)
        {
            expired.Unapply();
        }
        modifiers = modifiers.Except(expireds).ToList();
    }

    /// <summary>
    /// 모든 효과의 남은 턴 수를 감소시키고 0인 효과 제거
    /// </summary>
    public void NotifyTurnPassed()
    {
        foreach (var modifier in modifiers)
        {
            modifier.DecreaseDuration();
        }
        RemoveExpired();
    }
}