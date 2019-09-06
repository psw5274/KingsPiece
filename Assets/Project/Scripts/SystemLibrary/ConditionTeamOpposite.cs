using PieceSystem;
using UnityEngine;

namespace BoardSystem.Query
{
    [CreateAssetMenu(fileName = "Condition Team Opposite", menuName = "Board Layer/Condition Object/Team Opposite", order = 1)]
    public class ConditionTeamOpposite : Condition
    {
        public override bool Check(Piece self, BoardCoord coordination)
        {
            return self.GetTeamColor() != BoardManager.Instance.GetPieceAt(coordination).GetTeamColor();
        }
    }
}