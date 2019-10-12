using System;
using System.Linq;
using BoardSystem.Query;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New First Match Layer", menuName = "Board Layer/First Match", order = 3)]
    [Serializable]
    public class FirstMatchLayer : BoardLayer
    {
        public BoardLayer[] layers;
        public Condition condition;

        public override BoardCoord[] GetCoordinations(Piece self)
        {
            BoardCoord[] coords = new BoardCoord[] { };

            foreach (var layer in layers)
            {
                coords = layer.GetCoordinations(self)
                              .Where(coordination => condition.Check(self, coordination))
                              .ToArray();

                if (coords.Count() != 0)
                {
                    break;
                }
            }

            return coords;
        }
    }
}