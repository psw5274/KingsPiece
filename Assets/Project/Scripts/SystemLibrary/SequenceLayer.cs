
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
                              .Where(coordination => condition.Check(coordination));

                if (temp.Count() == 0)
                {
                    break;
                }

                coords = coords.Concat(temp).ToArray();
            }

#if DEBUG_ALL || DEBUG_QUERY_LAYER
            string DEBUG_STRING = $"Sequence Layer \"{name}\"";
            DEBUG_STRING += $"\nQueried count [{coords.Count()}]";
            DEBUG_STRING += $"\nQueried list below";
            foreach (var elem in coords)
            {
                DEBUG_STRING += $"\n({elem.col}, {elem.row})";
            }
            Debug.Log(DEBUG_STRING);
#endif

            return coords;
        }
    }
}
