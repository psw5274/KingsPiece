using System;
using System.Linq;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [Serializable]
    public abstract class BoardLayer : ScriptableObject
    {
        public abstract BoardCoord[] GetCoordinations(Piece self);
    }
}