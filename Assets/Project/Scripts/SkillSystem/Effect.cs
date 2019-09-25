using System;
using PieceSystem;
using UnityEngine;

namespace SkillSystem
{
    public class Effect
    {
        [Serializable]
        public enum Trigger
        {
            TurnStart,
            TurnEnd,
            Move,
            Attack,
            Hit,
            SkillUse,
            AnyAction
        }

        [Serializable]
        public class Duration
        {
            [Serializable]
            public enum Criteria
            {
                TurnBase,
                UseabilityBase
            }

            public Criteria criteria;
            public int value;

            public Duration(Criteria criteria = Criteria.TurnBase, int value = 1)
            {
                this.criteria = criteria;
                this.value = value;
            }
        }

        [Serializable]
        public enum Tag
        {
            Neutral,
            Benefical,
            Harmful,
            StatRelated,
            EffectRelated
        }

        public Trigger trigger;
        public Duration duration;
        public Tag[] tags;
        
        public System.Action<Effect, Piece> OnSetUp;
        public System.Action<Effect, Piece> OnTrigger;
        public System.Action<Effect, Piece> OnTearDown;

        public Effect(Trigger trigger, Duration duration, Tag[] tags, System.Action<Effect, Piece> OnSetUp, System.Action<Effect, Piece> OnTrigger, System.Action<Effect, Piece> OnTearDown)
        {
            this.trigger = trigger;
            this.duration = duration;
            this.tags = tags;
            this.OnSetUp = OnSetUp;
            this.OnTrigger = OnTrigger;
            this.OnTearDown = OnTearDown;
        }
    }
}