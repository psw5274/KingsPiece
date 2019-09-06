using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "RookClass", menuName = "Piece Class/Rook", order = 5)]
    public class RookClass : PieceClass
    {
        public BoardLayer attack;
        public BoardLayer move;

        public override BoardCoord[] GetAttackablePositions(Piece self)
        {
            return attack.GetCoordinations(self);
        }

        public override BoardCoord[] GetMovablePositions(Piece self)
        {
            return move.GetCoordinations(self);
        }
    }
}