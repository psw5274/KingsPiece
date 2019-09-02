using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "KingClass", menuName = "Piece Class/King", order = 1)]
    public class KingClass : PieceClass
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