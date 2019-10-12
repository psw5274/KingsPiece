using System;
using System.Linq;
using BoardSystem.Query;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Combination Layer", menuName = "Board Layer/Combination", order = 2)]
    [Serializable]
    public class CombinationLayer : BoardLayer
    {
        public BoardLayer[] layers;
        public Condition condition;

        public override BoardCoord[] GetCoordinations(Piece self)
        {
            BoardCoord[] coords = new BoardCoord[] { };

            foreach (var layer in layers)
            {
                coords = coords.Concat(layer.GetCoordinations(self).Where(coordination => condition.Check(self, coordination))).ToArray();
            }

            return coords;
        }
    }
}
