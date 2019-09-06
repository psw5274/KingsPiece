using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "KingClass", menuName = "Piece Class/King", order = 1)]
    public class KingClass : PieceClass
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