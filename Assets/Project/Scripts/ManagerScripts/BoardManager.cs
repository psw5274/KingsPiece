using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamColor : int { White = 1, Black = -1 }

enum PieceEnum
{
    NULL = 0, WhiteKing = 1, WhiteQueen, WhiteBishop,
    WhiteKnight, WhiteRook, WhitePawn,
    BlackKing = -1, BlackQueen = -2, BlackBishop = -3,
    BlackKnight = -4, BlackRook = -5, BlackPawn = -6
}

enum BoardEdge { Top = 36, Bottom = -36, LEFT = -36, RIGHT = 36 }

public enum Action { Move, Attack }


public class BoardManager : MonoBehaviour
{
    public const int NUM_BOARD_ROW = 8;
    public const int NUM_BOARD_COL = 8;
    public const int NUM_PIECE_PREFABS = 18; // or 32 for all pawns selectable

    /// <summary>
    /// Card Set from user's setting
    /// </summary>
    public HeroCard[] heroCards = new HeroCard[NUM_PIECE_PREFABS];

    public GameObject pieceUIPrefab;
    public GameObject moveHighlightFilter;
    public GameObject attackHighlightFilter;
    public GameObject pieceZone;

    /// <summary>
    /// Chess Board Status with pieces
    /// </summary>
    public GameObject[][] boardStatus;
    private List<GameObject> filterList = new List<GameObject>();

    [SerializeField]
    private BoardCoord selectedBoardCoord;
    public GameObject selectedPiece;
    public bool isPieceSelected = false;
    public bool isMagicReady = false;
    public bool isSkillReady = false;
    public MagicCard selectedMagicCard = null;
    public HeroCard selectedHeroCard = null;

    private static BoardManager instance = null;
    public static BoardManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(BoardManager)) as BoardManager;
                if (!instance)
                {
                    Debug.Log("ERROR : NO BoardManager");
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Initialize game board
    /// </summary>
    /// <returns></returns>
    public bool InitBoard()
    {

        boardStatus = new GameObject[NUM_BOARD_ROW][];
        for (int i = 0; i < NUM_BOARD_ROW; i++)
            boardStatus[i] = new GameObject[NUM_BOARD_COL];


        for (int i = 0; i < 8; i++)
        {
            boardStatus[i][0] =
            Instantiate(heroCards[i].heroModelPrefab,
                        BoardCoord.GetBoardCoordVector3(0, i),
                        heroCards[i].heroModelPrefab.transform.rotation,
                        pieceZone.transform);
            boardStatus[i][0].GetComponent<Piece>().Initialize(TeamColor.White,heroCards[i]);

            boardStatus[i][NUM_BOARD_ROW - 1] =
            Instantiate(heroCards[i + NUM_PIECE_PREFABS / 2].heroModelPrefab,
                        BoardCoord.GetBoardCoordVector3(NUM_BOARD_COL - 1, i),
                        heroCards[i + NUM_PIECE_PREFABS / 2].heroModelPrefab.transform.rotation,
                        pieceZone.transform);
            boardStatus[i][NUM_BOARD_ROW - 1].GetComponent<Piece>().Initialize(TeamColor.Black,
                                                                               heroCards[i+NUM_PIECE_PREFABS/2]);
        }

        // code for when num of user's piece set are 18
        if (NUM_PIECE_PREFABS == 18)
        {
            for (int i = 0; i < NUM_PIECE_PREFABS / 2 - 1; i++)    // instantiate pawn
            {
                boardStatus[i][1] =
                Instantiate(heroCards[8].heroModelPrefab,
                        BoardCoord.GetBoardCoordVector3(1, i),
                        heroCards[8].heroModelPrefab.transform.rotation,
                        pieceZone.transform);
                boardStatus[i][1].GetComponent<Piece>().Initialize(TeamColor.White,heroCards[8]);

                boardStatus[i][NUM_BOARD_ROW - 2] =
                Instantiate(heroCards[8 + NUM_PIECE_PREFABS / 2].heroModelPrefab,
                            BoardCoord.GetBoardCoordVector3(NUM_BOARD_COL - 2, i),
                            heroCards[8 + NUM_PIECE_PREFABS / 2].heroModelPrefab.transform.rotation,
                            pieceZone.transform);
                boardStatus[i][NUM_BOARD_ROW - 2].GetComponent<Piece>().Initialize(TeamColor.Black,
                                                                                    heroCards[8 + NUM_PIECE_PREFABS / 2]);

            }
        }

        ResetPiecesMovableCount();

        return true;
    }

    private bool SelectPiece(BoardCoord boardCoord)
    {
        selectedPiece = boardStatus[boardCoord.col][boardCoord.row];
        if (selectedPiece == null ||
            selectedPiece.GetComponent<Piece>().teamColor != GameManager.Instance.currentTurn)
        {
            isPieceSelected = false;
            return false;
        }

        isPieceSelected = true;
        return true;
    }

    private void UnselectPiece()
    {
        selectedPiece = null;
        isPieceSelected = false;
        isMagicReady = false;
        isSkillReady = false;
        selectedMagicCard = null;
        selectedHeroCard = null;
    }

    /// <summary>
    /// 보드 클릭시 피스 선택 및 활동 타일 선택
    /// </summary>
    private void OnMouseDown()
    {
        BoardCoord clickedCoord = GetClickedCoord();
        if (!clickedCoord.IsAvailable())
            return;

        ResetBoardHighlighter();
        selectedBoardCoord = clickedCoord;
        Piece selectedPieceScript = isPieceSelected ? selectedPiece.GetComponent<Piece>() : null;

        if (isMagicReady && selectedMagicCard != null)
        {
            if (boardStatus[selectedBoardCoord.col][selectedBoardCoord.row] == null)
            {
                ResetBoardHighlighter();
            }
            else
            {
                selectedMagicCard.magicData.Operate(selectedBoardCoord);

                CardManager.Instance.UseCard(selectedMagicCard);
            }
        }
        // 스킬
        else if (isSkillReady && selectedHeroCard != null)
        {
            if (boardStatus[selectedBoardCoord.col][selectedBoardCoord.row] == null)
            {
                ResetBoardHighlighter();
            }
            else
            {
                selectedHeroCard.skill.Operate(selectedBoardCoord);
            }
        }
        // 이동
        else if (isPieceSelected && selectedPieceScript.IsDetinationAvailable(selectedBoardCoord))
        {
            selectedPieceScript.Move(selectedBoardCoord);
            GameManager.Instance.isMoved = true;
        }
        // 공격
        else if (isPieceSelected && selectedPieceScript.IsAttackAvailable(selectedBoardCoord))
        { 
            selectedPieceScript.Attack(selectedBoardCoord);
            selectedPieceScript.UpdateStatus();

            GameManager.Instance.isMoved = true;
        }
        // 피스 선택
        else
        {
            if (!SelectPiece(selectedBoardCoord))
                return;
            selectedPieceScript = selectedPiece.GetComponent<Piece>();

            if (!GameManager.Instance.isMoved)
            {
                selectedPieceScript.GetAvailableDestination();
                HighlightBoard(selectedPieceScript.moveDestinationList, Action.Move);
                HighlightBoard(selectedPieceScript.attackTargetList, Action.Attack);
            }
            return;
        }

        UnselectPiece();
    }

    public void HighlightBoard(List<BoardCoord> highlightCoordList, Action action)
    {
        GameObject filter = action == Action.Move ? moveHighlightFilter : attackHighlightFilter;

        foreach (BoardCoord coord in highlightCoordList)
        {
            filterList.Add(Instantiate(filter,
                            coord.GetBoardCoardVector3(),
                            Quaternion.LookRotation(Vector3.up),
                            this.gameObject.transform));
        }
    }

    public void ResetBoardHighlighter()
    {
        foreach (GameObject filter in filterList)
        {
            Destroy(filter);
        }
        filterList.Clear();
    }

    public void ResetPiecesMovableCount()
    {
        foreach (var rows in BoardManager.Instance.boardStatus)
        {
            foreach (var pieceObject in rows)
            {
                if (pieceObject == null)
                {
                    continue;
                }

                pieceObject.GetComponent<Piece>().movableCount = 1;
            }
        }
    }

    // TODO : 손좀 보자
    public BoardCoord GetClickedCoord()
    {
        float mousePositionX = float.MaxValue;
        float mousePositionZ = float.MaxValue;
        BoardCoord clickedCoord = new BoardCoord(-1, -1);
        //if (!Camera.main) ;
        if (Camera.main)
        {
            RaycastHit hit;
            float raycastDistance = 1000.0f;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                                out hit, raycastDistance, LayerMask.GetMask("ChessBoard")))
            {
                mousePositionX = hit.point.x - transform.position.x;
                mousePositionZ = hit.point.z - transform.position.z;
            }
            else
                return clickedCoord;

            if (!((float)BoardEdge.LEFT <= mousePositionX && mousePositionX <= (float)BoardEdge.RIGHT &&
                  (float)BoardEdge.Bottom <= mousePositionZ && mousePositionZ <= (float)BoardEdge.Top))
                return clickedCoord;

            return new BoardCoord(mousePositionX, mousePositionZ);
        }
        return clickedCoord;
    }

    private void Awake()
    {
        InitBoard();
    }

}