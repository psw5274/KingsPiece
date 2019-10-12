using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill00009", menuName = "Piece Skill Card/Skill00009", order = 6)]
    public class Skill00009 : PieceSkill
    {
        public override void Operate(Piece self, BoardCoord[] targets)
        {
            OperateBegin(self, targets);
            OperateEnd(self, targets);
        }
    }
}
