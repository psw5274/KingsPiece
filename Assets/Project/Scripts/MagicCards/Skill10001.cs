using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill10001", menuName = "Magic Card/Skill10001", order = 5)]
    public class Skill10001 : MagicSkill
    {
        public int amount;

        public override void Operate(BoardCoord[] targets)
        {
            foreach (var target in targets)
            {
                GetPieceAt(target).AddHP(amount);
                GetPieceAt(target).HealHP(amount);
            }
        }
    }
}