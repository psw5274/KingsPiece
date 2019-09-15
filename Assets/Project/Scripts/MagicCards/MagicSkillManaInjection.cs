using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "MagicSkillManaInjection", menuName = "Magic Card/Mana Injection", order = 5)]
    public class MagicSkillManaInjection : MagicSkill
    {
        public int amount;

        public override void Operate(BoardCoord[] targets)
        {
            foreach (var target in targets)
            {
                GetPieceAt(target).AddATK(amount);
            }
        }
    }
}