using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "QueenClass", menuName = "Piece Class/Queen", order = 2)]
    public class QueenClass : PieceClass
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