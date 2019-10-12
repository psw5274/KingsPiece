using System;
using System.Linq;
using BoardSystem.Query;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Condition Layer", menuName = "Board Layer/Condition", order = 3)]
    [Serializable]
    public class ConditionLayer : BoardLayer
    {
        public BoardLayer layer;
        public Condition condition;

        public override BoardCoord[] GetCoordinations(Piece self)
        {
            BoardCoord[] coords = new BoardCoord[] { }; 

            coords = coords.Concat(layer.GetCoordinations(self)
                           .Where(coordination => condition.Check(self, coordination)))
                           .ToArray();

            return coords;
        }
    }
}