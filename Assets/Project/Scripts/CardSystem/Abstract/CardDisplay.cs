using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class CardDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
                                    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
                                    IPointerUpHandler
{
    public Image cardTemplateImage;
    public Image cardBackTemplateImage;
    public Image cardPortraitImage;

    public bool isFront;

    public Text nameText;
    public Text descriptionText;

    private static GameObject selectedCard;
    protected Vector3 dragBeginningPoint;
    private int cardSiblingIndex;

    public void ShowFront()
    {
        cardBackTemplateImage.transform.SetAsFirstSibling();
        isFront = true;
    }

    public void ShowBack()
    {
        cardBackTemplateImage.transform.SetSiblingIndex(100);
        isFront = false;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //dragBeginningPoint = this.transform.position;
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (true)
            this.transform.position = dragBeginningPoint;
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        dragBeginningPoint = this.transform.position;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardSiblingIndex = this.transform.GetSiblingIndex();
        this.transform.position += new Vector3(0, 20, 0);
        this.transform.SetAsLastSibling();
        selectedCard = this.gameObject;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.position -= new Vector3(0, 20, 0);
        this.transform.SetSiblingIndex(cardSiblingIndex);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {

    }
}
