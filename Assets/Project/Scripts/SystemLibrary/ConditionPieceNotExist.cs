using UnityEngine;

namespace BoardSystem.Query
{
    [CreateAssetMenu(fileName = "Condition Piece Not Exist", menuName = "Board Layer/Condition/Piece Not Exist", order = 1)]
    public class ConditionPieceNotExist : Condition
    {
        public override bool Check(BoardCoord coordination)
        {
            return BoardManager.Instance.GetPieceAt(coordination) == null;
        }
    }
}
