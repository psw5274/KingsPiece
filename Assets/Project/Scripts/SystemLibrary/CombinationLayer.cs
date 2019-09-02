using System;
using System.Linq;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Combination Layer", menuName = "Board Layer/Combination", order = 2)]
    [Serializable]
    public class CombinationLayer : BoardLayer
    {
        public BoardLayer[] layers;

        public override BoardCoord[] GetCoordinations()
        {
            BoardCoord[] coords = new BoardCoord[] { };

            foreach (var layer in layers)
            {
                coords.Concat(layer.GetCoordinations());
            }

            return coords;
        }
    }
}
