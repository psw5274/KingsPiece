using UnityEngine;

namespace PieceSystem
{
    public abstract class PieceClass : ScriptableObject
    {
        public abstract BoardCoord[] GetMovablePositions(Piece self);
        public abstract BoardCoord[] GetAttackablePositions(Piece self);
    }
}