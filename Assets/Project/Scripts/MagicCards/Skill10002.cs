using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill10002", menuName = "Magic Card/Skill10002", order = 5)]
    public class Skill10002 : MagicSkill
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