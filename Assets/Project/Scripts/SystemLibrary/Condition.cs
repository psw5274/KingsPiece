using UnityEngine;

namespace BoardSystem.Query
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool Check(BoardCoord coordination);
    }
}
