using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill00019", menuName = "Piece Skill Card/Skill00019", order = 6)]
    public class Skill00019 : PieceSkill
    {
        public override void Operate(Piece self, BoardCoord[] targets)
        {
            OperateBegin(self, targets);
            OperateEnd(self, targets);
        }
    }
}
