using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "QueenClass", menuName = "Piece Class/Queen", order = 2)]
    public class QueenClass : PieceClass
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