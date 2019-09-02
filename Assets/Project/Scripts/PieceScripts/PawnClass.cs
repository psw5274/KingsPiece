using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "PawnClass", menuName = "Piece Class/Pawn", order = 6)]
    public class PawnClass : PieceClass
    {
        public BoardLayer move;
        public BoardLayer moveFirst;

        public override BoardCoord[] GetAttackablePositions(Piece self)
        {
            throw new System.NotImplementedException();
        }

        public override BoardCoord[] GetMovablePositions(Piece self)
        {
            if ((self.GetStatus() & Piece.StatusFlag.Moved) == Piece.StatusFlag.Moved)
            {
                return move.GetCoordinations();
            }
            else
            {
                return moveFirst.GetCoordinations();
            }
        }
    }
}