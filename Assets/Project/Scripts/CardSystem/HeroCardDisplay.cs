using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroCardDisplay : CardDisplay
{
    public HeroCard cardData;

    public Text atkText;
    public Text hpText;

    void Start ()
    {
        nameText.text = cardData.cardName;
        descriptionText.text = cardData.cardDescription;

        cardTemplateImage.sprite = cardData.cardTemplateImage;
        cardBackTemplateImage.sprite = cardData.cardBackImage;
        cardPortraitImage.sprite = cardData.cardPortraitImage;

        atkText.text = cardData.statATK.ToString();
        hpText.text = cardData.statHP.ToString();
    }
}
