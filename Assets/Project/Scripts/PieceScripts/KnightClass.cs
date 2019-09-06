using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "KnightClass", menuName = "Piece Class/Knight", order = 4)]
    public class KnightClass : PieceClass
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