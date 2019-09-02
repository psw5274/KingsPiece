using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "KnightClass", menuName = "Piece Class/Knight", order = 4)]
    public class KnightClass : PieceClass
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