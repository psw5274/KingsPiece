using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "MagicSkillFreezing", menuName = "Magic Card/Freezing", order = 5)]
    public class MagicSkillFreezing : MagicSkill
    {
        public override void Operate(BoardCoord[] targets)
        {
            foreach (var target in targets)
            {
                // GetPieceAt(target).AddEffect(BufDebuf.Freezing);
            }
        }
    }
}