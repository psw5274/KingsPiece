using PieceSystem;
using UnityEngine;

namespace BoardSystem.Query
{
    [CreateAssetMenu(fileName = "Condition Piece Exist", menuName = "Board Layer/Condition Object/Piece Exist", order = 1)]
    public class ConditionPieceExist : Condition
    {
        public override bool Check(Piece self, BoardCoord coordination)
        {
            return BoardManager.Instance.GetPieceAt(coordination) != null;
        }
    }
}