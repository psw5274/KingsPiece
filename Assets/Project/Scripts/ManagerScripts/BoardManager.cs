using System.Collections.Generic;
using System.Linq;
using PieceSystem;
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
    [SerializeField]
    private GameObject selectedPiece;
    [SerializeField]
    private bool isPieceSelected = false;
    public bool isMagicReady = false;
    public MagicCard selectedMagicCard = null;


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

    public Piece GetPieceAt(BoardCoord position)
    {
        if (position.IsAvailable() == false)
        {
            return null;
        }

        var coordination = boardStatus[position.col][position.row];

        if (coordination == null)
        {
            return null;
        }
        else
        {
            return coordination.GetComponent<Piece>();
        }
    }

    public Piece GetPieceAt(int col, int row)
    {
        return GetPieceAt(new BoardCoord(col, row));
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
                        BoardCoord.GetBoardCoordVector3(i, 0),
                        heroCards[i].heroModelPrefab.transform.rotation,
                        pieceZone.transform);
            boardStatus[i][0].GetComponent<Piece>().Initialize(TeamColor.White, heroCards[i]);
            boardStatus[i][0].GetComponent<Piece>().MovePosition(new BoardCoord(i, 0));

            boardStatus[i][NUM_BOARD_ROW - 1] =
            Instantiate(heroCards[i + NUM_PIECE_PREFABS / 2].heroModelPrefab,
                        BoardCoord.GetBoardCoordVector3(i, NUM_BOARD_COL - 1),
                        heroCards[i + NUM_PIECE_PREFABS / 2].heroModelPrefab.transform.rotation,
                        pieceZone.transform);
            boardStatus[i][NUM_BOARD_ROW - 1].GetComponent<Piece>().Initialize(TeamColor.Black, heroCards[i + NUM_PIECE_PREFABS / 2]);
            boardStatus[i][NUM_BOARD_ROW - 1].GetComponent<Piece>().MovePosition(new BoardCoord(i, NUM_BOARD_COL - 1));
        }

        // code for when num of user's piece set are 18
        if (NUM_PIECE_PREFABS == 18)
        {
            for (int i = 0; i < NUM_PIECE_PREFABS / 2 - 1; i++)    // instantiate pawn
            {
                boardStatus[i][1] =
                Instantiate(heroCards[8].heroModelPrefab,
                        BoardCoord.GetBoardCoordVector3(i, 1),
                        heroCards[8].heroModelPrefab.transform.rotation,
                        pieceZone.transform);
                boardStatus[i][1].GetComponent<Piece>().Initialize(TeamColor.White, heroCards[8]);
                boardStatus[i][1].GetComponent<Piece>().MovePosition(new BoardCoord(i, 1));

                boardStatus[i][NUM_BOARD_ROW - 2] =
                Instantiate(heroCards[8 + NUM_PIECE_PREFABS / 2].heroModelPrefab,
                            BoardCoord.GetBoardCoordVector3(i, NUM_BOARD_COL - 2),
                            heroCards[8 + NUM_PIECE_PREFABS / 2].heroModelPrefab.transform.rotation,
                            pieceZone.transform);
                boardStatus[i][NUM_BOARD_ROW - 2].GetComponent<Piece>().Initialize(TeamColor.Black, heroCards[8 + NUM_PIECE_PREFABS / 2]);
                boardStatus[i][NUM_BOARD_ROW - 2].GetComponent<Piece>().MovePosition(new BoardCoord(i, NUM_BOARD_COL - 2));

            }
        }

        ResetPiecesMovableCount();

        return true;
    }

    private bool SelectPiece(BoardCoord boardCoord)
    {
        selectedPiece = boardStatus[boardCoord.col][boardCoord.row];
        if (selectedPiece == null ||
            selectedPiece.GetComponent<Piece>().GetTeamColor() != GameManager.Instance.currentTurn)
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
        selectedMagicCard = null;
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
                selectedMagicCard.skillData.Operate(new BoardCoord[] { selectedBoardCoord });

                CardManager.Instance.UseCard(selectedMagicCard);
            }
        }
        // 이동
        else if (isPieceSelected && selectedPieceScript.GetMovablePositions().Contains(selectedBoardCoord))
        {
            BoardManager.Instance.boardStatus[selectedPieceScript.GetPosition().col][selectedPieceScript.GetPosition().row] = null;
            BoardManager.Instance.boardStatus[selectedBoardCoord.col][selectedBoardCoord.row] = selectedPiece;
            selectedPiece.transform.position = selectedBoardCoord.GetBoardCoardVector3();
            selectedPieceScript.MovePosition(selectedBoardCoord);

            selectedPieceScript.SetStatus(Piece.StatusFlag.Moved);
            GameManager.Instance.isMoved = true;
        }
        // 공격
        else if (isPieceSelected && selectedPieceScript.GetAttackablePositions().Contains(selectedBoardCoord))
        {
            var targetPiece = GetPieceAt(selectedBoardCoord);
            targetPiece.DamageHP(selectedPieceScript.GetCurrentATK());

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
                HighlightBoard(selectedPieceScript.GetMovablePositions().ToList(), Action.Move);
                HighlightBoard(selectedPieceScript.GetAttackablePositions().ToList(), Action.Attack);
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

                pieceObject.GetComponent<Piece>().SetMovability(1);
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