
using System;
using System.Linq;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Sequence Layer", menuName = "Board Layer/Sequence", order = 3)]
    [Serializable]
    public class SequenceLayer : BoardLayer
    {
        public BoardLayer[] layers;

        public override BoardCoord[] GetCoordinations()
        {
            BoardCoord[] coords = new BoardCoord[] { };

            foreach (var layer in layers)
            {
                coords = layer.GetCoordinations();

                if (coords.Count() != 0)
                {
                    return coords;
                }
            }

            return coords;
        }
    }
}
