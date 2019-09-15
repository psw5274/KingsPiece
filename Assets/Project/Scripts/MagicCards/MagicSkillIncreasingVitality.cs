using UnityEngine;

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
        }
    }
}