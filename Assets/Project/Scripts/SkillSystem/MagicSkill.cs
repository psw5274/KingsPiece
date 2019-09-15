using System.Collections.Generic;
using System.Linq;
using BoardSystem;

namespace SkillSystem
{
    public abstract class MagicSkill : BaseSkill
    {
        public BoardLayer layer;

        public virtual List<BoardCoord> GetTargetCoordinations()
        {
            List<BoardCoord> coords = new List<BoardCoord>();

            coords = layer.GetCoordinations(GetCurrentKing()).ToList();

            return coords;
        }

        public abstract void Operate(BoardCoord[] targets);
    }
}