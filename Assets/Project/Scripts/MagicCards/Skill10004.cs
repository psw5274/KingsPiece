using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(fileName = "Skill10004", menuName = "Magic Card/Skill10004", order = 5)]
    public class Skill10004 : MagicSkill
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