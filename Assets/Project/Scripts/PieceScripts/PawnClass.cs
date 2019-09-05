using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "PawnClass", menuName = "Piece Class/Pawn", order = 6)]
    public class PawnClass : PieceClass
    {
        public BoardLayer attack;
        public BoardLayer move;
        public BoardLayer moveFirst;

        public override BoardCoord[] GetAttackablePositions(Piece self)
        {
            return attack.GetCoordinations(self);
        }

        public override BoardCoord[] GetMovablePositions(Piece self)
        {
            if ((self.GetStatus() & Piece.StatusFlag.Moved) == Piece.StatusFlag.Moved)
            {
                return move.GetCoordinations(self);
            }
            else
            {
                return moveFirst.GetCoordinations(self);
            }
        }
    }
}