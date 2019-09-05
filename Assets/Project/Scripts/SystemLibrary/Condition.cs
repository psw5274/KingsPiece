using UnityEngine;

namespace BoardSystem.Query
{
    [CreateAssetMenu(fileName = "Condition Pass", menuName = "Board Layer/Condition/Pass", order = 1)]
    public class Condition : ScriptableObject
    {
        public virtual bool Check(BoardCoord coordination)
        {
            return true;
        }
    }
}
