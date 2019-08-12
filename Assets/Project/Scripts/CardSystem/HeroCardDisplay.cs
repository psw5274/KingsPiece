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

        atkText.text = cardData.statHP.ToString();
        hpText.text = cardData.statHP.ToString();
    }

    /// <summary>
    /// 카드 사용(마우스 클릭 뗄때)
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (dragBeginningPoint != this.transform.position)
        {
            Debug.Log(dragBeginningPoint);
            Debug.Log(this.transform.position);
            return;
        }

        BoardManager.Instance.selectedPiece.GetComponent<Piece>().SkillReady();

    }
}
