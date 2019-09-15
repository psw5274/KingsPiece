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
        }
    }
}