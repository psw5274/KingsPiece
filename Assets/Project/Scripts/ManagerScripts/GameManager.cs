using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject chessBoard;
    public Text winningText;

    [SerializeField]
    public static TeamColor currentTurn { get; private set; } = TeamColor.White;

    public bool isSkillUsed = false;
    public bool isMoved = false;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!instance)
                {
                    Debug.Log("ERROR : NO GameManager");
                }
            }
            return instance;
        }
    }

    public void SetWinner(TeamColor winTeam)
    {
        winningText.text = winTeam.ToString() + " Win!!";
        winningText.gameObject.SetActive(true);
    }

    public void EndTurn()
    {
        BoardManager.Instance.ResetBoardHighlighter();
        BoardManager.Instance.ResetPiecesMovableCount();

        ChangeTurn();
    }

    private void ChangeTurn()
    {
        currentTurn = (currentTurn == TeamColor.Black) ? TeamColor.White : TeamColor.Black;

        // PlayerManager.Instance.ChangePlayerTeam();

        NewTurn();
    }

    private void NewTurn()
    {
        // 카드 드로우, 버프 등의 함수 호출 해주자

        EffectManager.Instance.NotifyTurnPassed();

        isSkillUsed = false;
        isMoved = false;
    }

    public void SetPlayer()
    {
        CardManager.Instance.ShowPlayerHands();
        CameraManager.Instance.SetCamera();
    }

    public void Start()
    {
        SetPlayer();
    }

    public bool IsPlayerTurn()
    {
        return currentTurn == PlayerManager.playerTeam ? true : false;
    }
}


/*
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private BoardManager boardManager;

    private GameObject prevFocusObject;
    private GameObject tmpCube;

    public static GameObject focusObject = null;
    public GameObject tmpCubePrf;
    
    // Singleton
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (!_instance)
                {
                    Debug.Log("ERROR : NO GameManager");
                }
            }
            return _instance;
        }
    }
    // Singleton

    void TmpObjectFocusing()
    {
        tmpCube = Instantiate(tmpCubePrf, focusObject.transform.position+ new Vector3(0, 15, 0),
                    Quaternion.identity, focusObject.transform);
    }

    public bool IsVictory(int king)
    {
        for(int i = 0; i < 8; i++)
            for(int j = 0; j < 8; j++)
                if (boardManager.boardStatus[i][j] == -king)
                    return false;
        return true;
    }

	void Awake ()
    {
        boardManager = BoardManager.Instance;
    }
	
	void Update ()
    {
        if(focusObject != prevFocusObject)
        {
            prevFocusObject = focusObject;
            Destroy(tmpCube);
            TmpObjectFocusing();
        }
	}
}
*/