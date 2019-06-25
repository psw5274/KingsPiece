using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardGrade { Common, Uncommon }

public abstract class BasicCardData : ScriptableObject
{
    public string cardName;
    public string cardDescription;

    public Sprite cardTemplateImage;
    public Sprite cardBackImage;
    public Sprite cardPortraitImage;

    public CardGrade cardGrade;
}
