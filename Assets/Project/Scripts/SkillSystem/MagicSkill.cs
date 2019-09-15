using System.Collections.Generic;
using System.Linq;
using BoardSystem;
#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
using UnityEngine;
#endif

namespace SkillSystem
{
    public abstract class MagicSkill : BaseSkill
    {
        public BoardLayer layer;

        public virtual List<BoardCoord> GetTargetCoordinations()
        {
            List<BoardCoord> coords = new List<BoardCoord>();

            coords = layer.GetCoordinations(GetCurrentKing()).ToList();

#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
            string DEBUG_STRING = $"Target Query \"{name}\"";
            DEBUG_STRING += $"\nQueried count [{coords.Count()}]";
            DEBUG_STRING += $"\nQueried list below";
            foreach (var ELEM in coords)
            {
                DEBUG_STRING += $"\n({ELEM.col}, {ELEM.row})";
            }
            Debug.Log(DEBUG_STRING);
#endif

            return coords;
        }

        public abstract void Operate(BoardCoord[] targets);
    }
}