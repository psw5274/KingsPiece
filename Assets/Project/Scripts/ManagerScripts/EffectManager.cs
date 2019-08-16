using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance;
    public static EffectManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(EffectManager)) as EffectManager;
                if (!instance)
                {
                    throw new System.NullReferenceException("ERROR : NO EffectManager");
                }
            }
            return instance;
        }
    }

    public List<Effect> effects = new List<Effect>();

    public void AddEffect(Piece target, Skill skill)
    {
#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"";
            if (target == null)
            {
                DEBUG_STRING += $"\ntarget is null";
            }
            else
            {
                DEBUG_STRING += $"\n {target} at ({target.pieceCoord.col}, {target.pieceCoord.row}) now has Effect";
            }
            if (skill == null)
            {
                DEBUG_STRING += $"\nskill is null";
            }
            else
            {
                DEBUG_STRING +=
                $"\n Skill" +
                $"\n - label : {skill.label}" +
                $"\n - timing : {skill.timing.ToString()}" +
                $"\n - duration : {skill.duration}" +
                $"\n - parameters :";
                foreach (var parameter in skill.parameters)
                {
                    DEBUG_STRING += $"\n   * {parameter.target.ToString()} : {parameter.value} * ";
                }
            }
            Debug.Log(DEBUG_STRING);
        }
#endif

        if (skill == null)
        {
            return;
        }

        foreach (Skill.Parameter parameter in skill.parameters)
        {
            Effect effect = null;
            switch (parameter.target)
            {
                case Skill.Parameter.ModifyTarget.ATKAddition:
                    effect = new EffectATKAddition(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.ATKMultiplication:
                    effect = new EffectATKMultiplication(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.HPAddition:
                    effect = new EffectHPAddition(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.HPMultiplication:
                    effect = new EffectHPMultiplication(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.Movability:
                    effect = new EffectImmovable(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.Unbeatability:
                    effect = new EffectUnbeatable(target, skill.timing, skill.duration);
                    break;
                case Skill.Parameter.ModifyTarget.HPDamage:
                    effect = new EffectHPDamage(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.HPHeal:
                    effect = new EffectHPHeal(target, skill.timing, skill.duration, parameter.value);
                    break;
                case Skill.Parameter.ModifyTarget.ActorPositionModification:
                    effect = new EffectActorPositionModification(target, skill.timing, skill.duration);
                    break;
                default:
                    continue;
            }

            if (skill.timing == Skill.ApplyingTiming.Instant)
            {
                effect.Active();
            }

            effects.Add(effect);
        }
    }

    public void NotifyAttacking(Piece target)
    {
#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"Effect manager notified {target} is attacking with {target.CurrentATK} ATK";
            Debug.Log(DEBUG_STRING);
        }
#endif
        var correspondings = effects.Where(effect => effect.timing == Skill.ApplyingTiming.Attack)
                                    .Where(effect => effect.target.Equals(target));

        foreach (var effect in correspondings)
        {
            effect.Active();
        }
    }

    public void NotifyDamaged(Piece target)
    {
#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"Effect manager notified {target} is damaged, now {target.CurrentHP} HP is remaing";
            Debug.Log(DEBUG_STRING);
        }
#endif
        var correspondings = effects.Where(effect => effect.timing == Skill.ApplyingTiming.Damaged)
                               .Where(effect => effect.target.Equals(target));

        foreach (var effect in correspondings)
        {
            effect.Active();
        }
    }

    public void NotifyMoved(Piece target)
    {
#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"Effect manager notified {target} is moved to ({target.pieceCoord.col}, {target.pieceCoord.row})";
            Debug.Log(DEBUG_STRING);
        }
#endif
        var correspondings = effects.Where(effect => effect.timing == Skill.ApplyingTiming.Moved)
                               .Where(effect => effect.target.Equals(target));

        foreach (var effect in correspondings)
        {
            effect.Active();
        }
    }

    public void NotifyTurnPassed()
    {
#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = "Effect manager notified turn passed";
            Debug.Log(DEBUG_STRING);
        }
#endif
        var correspondings = effects.Where(effect => effect.timing == Skill.ApplyingTiming.TurnPassed);

        foreach (var effect in correspondings)
        {
            effect.Active();
        }

        var expireds = effects.Where(effect => effect.remainDuration == 0 || effect.remainDuration == 1);

        if (expireds.Count() != 0)
        {
#if CONSOLE_LOGGING
            {
                string DEBUG_STRING = "Effect manager remove expired effects" +
                    "\n EXPIRED EFFECTS";
                foreach (var effect in expireds)
                {
                    DEBUG_STRING += $"\n * {effect.GetType()} effect to {effect.target}";
                    if (effect.target == null)
                    {
                        DEBUG_STRING += $"target and target is null";
                    }
                    else
                    {
                        DEBUG_STRING += $" at ({effect.target.pieceCoord.col}, {effect.target.pieceCoord.row})";
                    }
                }
                Debug.Log(DEBUG_STRING);
            }
#endif
            foreach (var effect in expireds)
            {
                effect.Deactive();
            }

            effects = effects.AsEnumerable().Except(expireds).ToList();
        }
        effects.ForEach(effect => --effect.remainDuration);
    }

    public void NotifyAnyAction(Piece target)
    {
#if CONSOLE_LOGGING
        {
            string DEBUG_STRING = $"Effect manager notified {target} do some action";
            Debug.Log(DEBUG_STRING);
        }
#endif
        var correspondings = effects.Where(effect => effect.timing == Skill.ApplyingTiming.AnyAction)
                               .Where(effect => effect.target.Equals(target));

        foreach (var effect in correspondings)
        {
            effect.Active();
        }
    }
}