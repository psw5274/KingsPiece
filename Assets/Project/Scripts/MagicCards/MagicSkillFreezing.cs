using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "MagicSkillFreezing", menuName = "Magic Card/Freezing", order = 5)]
    public class MagicSkillFreezing : MagicSkill
    {
        private Effect effect = new Effect(
            Effect.Trigger.TurnStart,
            new Effect.Duration(Effect.Duration.Criteria.TurnBase, 2),
            new Effect.Tag[] { Effect.Tag.Harmful, Effect.Tag.EffectRelated },
            (self, piece) => {
                piece.AddMovability(-1);
            },
            (self, piece) => {
                piece.AddMovability(-1);
                --self.duration.value;
            },
            (self, piece) => {
                piece.AddMovability(1);
            }
            );

        public override void Operate(BoardCoord[] targets)
        {
            foreach (var target in targets)
            {
                GetPieceAt(target).AddEffect(effect);
            }
        }
    }
}