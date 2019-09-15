using System.Collections.Generic;
using System.Linq;
using BoardSystem;
#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
using UnityEngine;
#endif

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "MagicSkillIncrease Vitality", menuName = "Magic Card/Increase Vitality", order = 5)]
    public class MagicSkillIncreasingVitality : MagicSkill
    {
        public BoardLayer layer;
        public int amount;

        public override List<BoardCoord> GetTargetCoordinations()
        {
            List<BoardCoord> coords = new List<BoardCoord>();

            coords = layer.GetCoordinations(GetCurrentKing()).ToList();

#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
            string DEBUG_STRING = $"Magic Skill Sniping Target Query\"{name}\"";
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

        public override void Operate(BoardCoord[] targets)
        {
            foreach (var target in targets)
            {
                GetPieceAt(target).AddHP(amount);
                GetPieceAt(target).HealHP(amount);
            }

#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
            string DEBUG_STRING = $"Increasing Vitality Operate \"{name}\"";
            DEBUG_STRING += $"\nTarget count [{targets.Count()}]";
            DEBUG_STRING += $"\nAdd {amount} HP";
            DEBUG_STRING += $"\nTarget list below";
            foreach (var ELEM in targets)
            {
                DEBUG_STRING += $"\n{GetPieceAt(ELEM)} at ({ELEM.col}, {ELEM.row})";
            }
            Debug.Log(DEBUG_STRING);
#endif
        }
    }
}