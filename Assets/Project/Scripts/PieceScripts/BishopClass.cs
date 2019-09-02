using UnityEngine;

namespace PieceSystem
{
    [CreateAssetMenu(fileName = "BishopClass", menuName = "Piece Class/Bishop", order = 3)]
    public class BishopClass : PieceClass
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