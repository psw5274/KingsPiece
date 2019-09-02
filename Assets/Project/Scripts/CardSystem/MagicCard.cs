using UnityEngine;
using SkillSystem;


[CreateAssetMenu(fileName = "MagicCard", menuName = "Card/MagicCard")]
public class MagicCard : BasicCardData
{
    public MagicSkill skillData;
}