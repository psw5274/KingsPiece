using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill10003", menuName = "Magic Card/Skill10003", order = 5)]
    public class Skill10003 : MagicSkill
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