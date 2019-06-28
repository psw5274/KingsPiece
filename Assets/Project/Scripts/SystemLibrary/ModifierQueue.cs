using System.Collections.Generic;
using System.Linq;

public class ModifierQueue
{
    private List<Modifier> modifiers = new List<Modifier>();

    public void Push(Modifier modifier)
    {
        modifier.Apply();
        modifiers.Add(modifier);
    }

    private void RemoveExpired()
    {
        var expireds = modifiers.Where(modifier => modifier.GetDuration() == 0);
        foreach (var expired in expireds)
        {
            expired.Unapply();
        }
        modifiers = modifiers.Except(expireds).ToList();
    }

    public void NotifyTurnPassed()
    {
        foreach (var modifier in modifiers)
        {
            modifier.DecreaseDuration();
        }
        RemoveExpired();
    }
}