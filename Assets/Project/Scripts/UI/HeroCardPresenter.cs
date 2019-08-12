using System.Collections;
using UnityEngine;

class HeroCardPresenter : MonoBehaviour
{
    [SerializeField]
    private HeroCardDisplay displayPrefab = null;
    [SerializeField]
    private RectTransform anchor = null;
    private HeroCardDisplay display = null;

    private void Start()
    {
        StartCoroutine(ShowIfSelected());
    }

    private IEnumerator ShowIfSelected()
    {
        while (true)
        {
            yield return new WaitUntil(() => BoardManager.Instance.isPieceSelected);
            ShowCard();
            yield return new WaitWhile(() => BoardManager.Instance.isPieceSelected);
            UnshowCard();
        }
    }

    private void ShowCard()
    {
        display = Instantiate<HeroCardDisplay>(displayPrefab, anchor);
        display.cardData = BoardManager.Instance.selectedPiece.GetComponent<Piece>().cardData;
        display.ShowFront();
        var rectTransform = display.gameObject.GetComponent<RectTransform>();
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.pivot = Vector2.one / 2.0f;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
    }

    private void UnshowCard()
    {
        Destroy(display.gameObject);
    }
}