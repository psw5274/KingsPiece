using PieceSystem;
using UnityEngine;

namespace BoardSystem.Query
{
    [CreateAssetMenu(fileName = "Condition Pass", menuName = "Board Layer/Condition Object/Pass", order = 1)]
    public class Condition : ScriptableObject
    {
        public virtual bool Check(Piece self, BoardCoord coordination)
        {
            return true;
        }
    }
}
