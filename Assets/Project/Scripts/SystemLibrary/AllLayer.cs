using System.Linq;
using PieceSystem;
using UnityEngine;

namespace BoardSystem
{
    [CreateAssetMenu(fileName = "New All Layer", menuName = "Board Layer/All", order = 2)]
    public class AllLayer : BoardLayer
    {
        public override BoardCoord[] GetCoordinations(Piece self)
        {
            BoardCoord[] coords = new BoardCoord[] { };

            for (int col = 0; col < BoardManager.NUM_BOARD_COL; ++col)
            {
                for (int row = 0; row < BoardManager.NUM_BOARD_ROW; ++row)
                {
                    coords = coords.Append(new BoardCoord(col, row)).ToArray();
                }
            }

#if DEBUG_ALL || DEBUG_QUERY_LAYER
            string DEBUG_STRING = $"All Layer \"{name}\"";
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