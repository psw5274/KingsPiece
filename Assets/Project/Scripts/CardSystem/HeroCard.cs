using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HeroCamp { Human, Devil }
public enum PieceClass { King, Queen, Bishop, Rook, Knight, Pawn };
[CreateAssetMenu(fileName = "HeroCard", menuName = "Card/HeroCard")]
public class HeroCard : BasicCardData
{
    public PieceClass heroClass;
    public HeroCamp heroCamp;
    public int statHP, statATK;

    public GameObject heroModelPrefab;

    public GameObject pieceAttackEffect;

    protected List<object> skillList = new List<object>();

    
    public void Awake()
    {
    }
}
