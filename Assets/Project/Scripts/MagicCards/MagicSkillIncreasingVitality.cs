using System.Linq;
#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
using UnityEngine;
#endif

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "MagicSkillIncreaseVitality", menuName = "Magic Card/Increase Vitality", order = 5)]
    public class MagicSkillIncreasingVitality : MagicSkill
    {
        public int amount;

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