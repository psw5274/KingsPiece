using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "RookClass", menuName = "Piece Class/Rook", order = 5)]
    public class RookClass : PieceClass
    {
        public override BoardCoord[] GetAttackablePositions(Piece self)
        {
            throw new System.NotImplementedException();
        }

        public override BoardCoord[] GetMovablePositions(Piece self)
        {
            throw new System.NotImplementedException();
        }
    }
}