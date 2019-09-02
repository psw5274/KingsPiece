using UnityEngine;
using SkillSystem;
using PieceSystem;

public enum HeroCamp { Human, Devil }
[CreateAssetMenu(fileName = "HeroCard", menuName = "Card/HeroCard")]
public class HeroCard : BasicCardData
{
    public PieceClass heroClass;
    public HeroCamp heroCamp;
    public int statHP, statATK;

    public GameObject heroModelPrefab;

    public PieceSkill[] skills;

    public void Awake()
    {
    }
}
