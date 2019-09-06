using PieceSystem;
using UnityEngine;

namespace BoardSystem.Query
{
    [CreateAssetMenu(fileName = "Condition Team Self", menuName = "Board Layer/Condition Object/Team Self", order = 1)]
    public class ConditionTeamSelf : Condition
    {
        public override bool Check(Piece self, BoardCoord coordination)
        {
            return self.GetTeamColor() == BoardManager.Instance.GetPieceAt(coordination).GetTeamColor();
        }
    }
}