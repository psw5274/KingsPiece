using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MagicCardDisplay : CardDisplay
{
    public MagicCard cardData;

    void Start()
    {
        nameText.text = cardData.cardName;
        descriptionText.text = cardData.cardDescription;

        cardTemplateImage.sprite = cardData.cardTemplateImage;
        cardBackTemplateImage.sprite = cardData.cardBackImage;
        cardPortraitImage.sprite = cardData.cardPortraitImage;
    }

    /// <summary>
    /// 카드 사용(마우스 클릭 뗄때)
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        // 앞면 카드 아닐 때
        if (!isFront || !GameManager.Instance.IsPlayerTurn())
            return;


        if (originalPosition != this.transform.position)
        {
            Debug.Log(originalPosition);
            Debug.Log(this.transform.position);
            return;
        }

        BoardManager.Instance.ResetBoardHighlighter();
        BoardManager.Instance.HighlightBoard(cardData.magicData.GetAvailableTargetCoord(), Action.Attack);
        BoardManager.Instance.isMagicReady = true;
        BoardManager.Instance.selectedMagicCard = cardData;
    }
}