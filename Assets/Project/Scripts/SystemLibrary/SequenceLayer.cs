using System;
using System.Linq;
using BoardSystem.Query;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New Sequence Layer", menuName = "Board Layer/Sequence", order = 3)]
    [Serializable]
    public class SequenceLayer : BoardLayer
    {
        public BoardLayer[] layers;
        public Condition condition;

        public override BoardCoord[] GetCoordinations(Piece self)
        {
            BoardCoord[] coords = new BoardCoord[] { };

            foreach (var layer in layers)
            {
                var temp = layer.GetCoordinations(self)
                              .Where(coordination => condition.Check(self, coordination));

                if (temp.Count() == 0)
                {
                    break;
                }

                coords = coords.Concat(temp).ToArray();
            }

            return coords;
        }
    }
}
