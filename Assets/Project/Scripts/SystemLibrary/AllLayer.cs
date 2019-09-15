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

            return coords;
        }
    }
}