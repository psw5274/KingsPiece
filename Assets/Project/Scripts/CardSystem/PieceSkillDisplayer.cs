using System.Collections;
using PieceSystem;
using UnityEngine;

public class PieceSkillDisplayer : MonoBehaviour
{
    public GameObject heroCardPrefab = null;
    public GameObject screener = null;
    private GameObject instance = null;
    private IEnumerator waitUntilPieceSelected;
    private IEnumerator waitWhilePieceSelected;

    public void Awake()
    {
        waitUntilPieceSelected = new WaitUntil(() => BoardManager.Instance.isPieceSelected);
        waitWhilePieceSelected = new WaitWhile(() => BoardManager.Instance.isPieceSelected);
        screener.SetActive(false);
    }
    public void OnEnable()
    {
        StartCoroutine(DisplayRoutine());
    }

    public void OnDisable()
    {
        StopAllCoroutines();
    }

    public IEnumerator DisplayRoutine()
    {
        while (true)
        {
            yield return waitUntilPieceSelected;
            screener.SetActive(true);

            instance = Instantiate(heroCardPrefab, transform);
            instance.transform.SetAsFirstSibling();

            var display = instance.GetComponent<HeroCardDisplay>();
            display.cardData = BoardManager.Instance.selectedHeroCard;

            var rt = instance.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.sizeDelta = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;

            yield return waitWhilePieceSelected;

            screener.SetActive(false);
            BoardManager.Instance.isPieceSkillReady = false;
            Destroy(instance);
        }
    }

    public void OnMouseDown()
    {
        BoardManager.Instance.isPieceSkillReady = true;
        BoardManager.Instance.ResetBoardHighlighter();
        BoardManager.Instance.HighlightBoard(instance.GetComponent<HeroCardDisplay>()
                                                     .cardData
                                                     .skills[0]
                                                     .GetTargetCoordinations(BoardManager.Instance.selectedPiece.GetComponent<Piece>()), Action.Attack);
    }
}