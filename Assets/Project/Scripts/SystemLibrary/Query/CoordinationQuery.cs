using System;

namespace Coordination.Query
{
    public class CoordinationQuery
    {
        public QueryLayer[] layers;
        private CoordinationGrid grid;

        public CoordinationGrid GetQueriedList()
        {
            if (grid == null)
            {
                SetGrid();
            }

            return grid;
        }

        public bool Exist(BoardCoord coordination)
        {
            if (grid == null)
            {
                SetGrid();
            }

            return Array.Exists(grid.positions, target => target.x == coordination.col && target.y == coordination.row);
        }

        private void SetGrid()
        {
            grid = new AllLayer().Query();

            foreach (var layer in layers)
            {
                grid = layer.Query(grid);
            }
        }
    }
}