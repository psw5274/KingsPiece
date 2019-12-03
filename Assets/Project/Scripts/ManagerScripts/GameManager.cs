using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Manager<GameManager>
{
    public GameObject chessBoard;
    public Text winningText;

    public TeamColor currentTurn = TeamColor.White;

    public bool isSkillUsed = false;
    public bool isMoved = false;

    public void SetWinner(TeamColor winTeam)
    {
        winningText.text = winTeam.ToString() + " Win!!";
        winningText.gameObject.SetActive(true);
    }

    public void NewTurn()
    {
        currentTurn = (currentTurn == TeamColor.Black) ? TeamColor.White : TeamColor.Black;

        BoardManager.Instance.ResetBoardHighlighter();
        BoardManager.Instance.ResetPiecesMovableCount();

        EffectManager.Instance.NotifyTurnPassed();
        isSkillUsed = false;
        isMoved = false;
    }

}