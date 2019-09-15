using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "PieceSkillLamentOfBanshee", menuName = "Piece Skill Card/Lament of Banshee", order = 6)]
    public class LamentOfBanshee : PieceSkill
    {
        public override void Operate(Piece self, BoardCoord[] targets)
        {
            OperateBegin(self, targets);
            OperateEnd(self, targets);
        }
    }
}
