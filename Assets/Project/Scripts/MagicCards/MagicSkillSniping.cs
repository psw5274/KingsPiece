using System.Linq;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "MagicSkillSniping", menuName = "Magic Card/Sniping", order = 5)]
    public class MagicSkillSniping : MagicSkill
    {
        public int damage;

        public override void Operate(BoardCoord[] targets)
        {
            foreach (var target in targets)
            {
                GetPieceAt(target).DamageHP(damage);
                GetPieceAt(target).UpdateStatus();
            }

#if DEBUG_ALL || DEBUG_SKILL || DEBUG_SKILL_MAGIC
            string DEBUG_STRING = $"Magic Skill Sniping Operate \"{name}\"";
            DEBUG_STRING += $"\nTarget count [{targets.Count()}]";
            DEBUG_STRING += $"\nDamage {damage} HP";
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