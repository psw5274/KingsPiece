using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {
    // 어차피 덱은 고정 길이
    // public List<BasicCardData> whiteCardDeck = new List<BasicCardData>();
    // public List<BasicCardData> blackCardDeck = new List<BasicCardData>();

    public const int DECK_SIZE = 10;    // 30 or 50;
    public GameObject heroCardPrfab;
    public GameObject magicCardPrefab;
    public GameObject deckZone;
    public GameObject handsZone;
    public GameObject oppositeHandsZone;

    public BasicCardData[] whiteCardDeck = new BasicCardData[DECK_SIZE];
    public BasicCardData[] blackCardDeck = new BasicCardData[DECK_SIZE];

    // 멀티플레이시 수정
    public List<BasicCardData> whiteHands = new List<BasicCardData>();
    public List<BasicCardData> blackHands = new List<BasicCardData>();

    public List<GameObject> playerCardList = new List<GameObject>();
    public List<GameObject> oppositeCardList = new List<GameObject>();


    private static CardManager instance = null;
    public static CardManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(CardManager)) as CardManager;
                if (!instance)
                {
                    Debug.Log("ERROR : NO BoardManager");
                }
            }
            return instance;
        }
    }

    public void UseCard(BasicCardData usedCard)
    {
        List<BasicCardData> playerHands = GameManager.Instance.currentTurn == TeamColor.White ?
                                          whiteHands : blackHands;
        playerHands.Remove(usedCard);
        ShowPlayerHands();
    }

    public void ShowPlayerHands()
    {
        List<BasicCardData> hands = PlayerManager.Instance.playerTeam == TeamColor.White ?
                                          whiteHands : blackHands;

        //  최적화 필수
        foreach (GameObject card in playerCardList)   
        {
            Destroy(card);
        }
        playerCardList.Clear();


        for(int i = 0; i < hands.Count; i++)
        {
            playerCardList.Add(Instantiate(magicCardPrefab, handsZone.transform));
            playerCardList[i].GetComponent<MagicCardDisplay>().cardData = (MagicCard)hands[i];

            playerCardList[i].transform.position = handsZone.transform.position + 
                                                   new Vector3((i - hands.Count/2) * 20, 0, 1);
            playerCardList[i].transform.RotateAround(handsZone.transform.position, new Vector3(0, 0, 1),
                                                     -(i - hands.Count / 2));

            playerCardList[i].GetComponent<MagicCardDisplay>().ShowFront();
        }

        // opposite player's hand's back
        List<BasicCardData> oppositeHands = PlayerManager.Instance.playerTeam == TeamColor.White ?
                                            blackHands : whiteHands;

        foreach (GameObject card in oppositeCardList)
        {
            Destroy(card);
        }
        oppositeCardList.Clear();


        for (int i = 0; i < oppositeHands.Count; i++)
        {
            oppositeCardList.Add(Instantiate(magicCardPrefab, oppositeHandsZone.transform));


            oppositeCardList[i].GetComponent<MagicCardDisplay>().cardData = (MagicCard)oppositeHands[i];

            oppositeCardList[i].transform.position = oppositeHandsZone.transform.position +
                                                    new Vector3((i - oppositeHands.Count / 2) * 20, 0, 1);
            oppositeCardList[i].transform.RotateAround(oppositeHandsZone.transform.position, 
                                                       new Vector3(0, 0, 1),
                                                       -(i - oppositeHands.Count / 2));
            oppositeCardList[i].GetComponent<MagicCardDisplay>().ShowBack();
        }
    }

    void Start()
    {
        // Get Card List Data From Somewhere... Like player setting..
        whiteHands.AddRange(whiteCardDeck);
        blackHands.AddRange(blackCardDeck);
        // Get Card List Data From Somewhere... Like player setting..

        ShowPlayerHands();
    }
}
