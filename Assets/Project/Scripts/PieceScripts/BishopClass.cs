using BoardSystem;
using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "BishopClass", menuName = "Piece Class/Bishop", order = 3)]
    public class BishopClass : PieceClass
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