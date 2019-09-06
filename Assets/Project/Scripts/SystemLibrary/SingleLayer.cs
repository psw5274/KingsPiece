using System;
using PieceSystem;
using UnityEngine;
#if DEBUG_ALL || DEBUG_QUERY_LAYER
using System.Linq;
#endif

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

#if DEBUG_ALL || DEBUG_QUERY_LAYER
            string DEBUG_STRING = $"Single Layer \"{name}\"";
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
