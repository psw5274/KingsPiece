using System;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Single Layer", menuName = "Board Layer/Single", order = 5)]
    [Serializable]
    public class SingleLayer : BoardLayer
    {
        public BoardCoord coordination;

        public override BoardCoord[] GetCoordinations(Piece self)
        {
            BoardCoord[] coords;

            if ((self.GetPosition() + coordination).IsAvailable())
            {
                coords = new BoardCoord[] { self.GetPosition() + coordination };
            }
            else
            {
                coords = new BoardCoord[] { };
            }

            return coords;
        }
    }
}
