using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill00007", menuName = "Piece Skill Card/Skill00007", order = 6)]
    public class Skill00007 : PieceSkill
    {
        public override void Operate(Piece self, BoardCoord[] targets)
        {
            OperateBegin(self, targets);
            OperateEnd(self, targets);
        }
    }
}