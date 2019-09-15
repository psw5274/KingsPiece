using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "PieceSkillRush", menuName = "Piece Skill Card/Rush", order = 6)]
    public class Rush : PieceSkill
    {
        public override void Operate(Piece self, BoardCoord[] targets)
        {
            OperateBegin(self, targets);
            OperateEnd(self, targets);
        }
    }
}