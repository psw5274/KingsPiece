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

    public void NewTurn(bool isNetworkCommmand = false)
    {
        if(!isNetworkCommmand)
        {
            NetworkManager.Instance.SendingPacketEnqueue(new NetworkPacket(PacketType.TURN_END, 0));
            
        }

        currentTurn = (currentTurn == TeamColor.Black) ? TeamColor.White : TeamColor.Black;

        BoardManager.Instance.ResetBoardHighlighter();
        BoardManager.Instance.ResetPiecesMovableCount();

        UIManager.Instance.SwitchEndTurnButtonText();

        EffectManager.Instance.NotifyTurnPassed();
        isSkillUsed = false;
        isMoved = false;

        Debug.Log("New Turn!");
    }

    public bool IsPlayerTurn()
    {
        if (PlayerManager.Instance.GetPlayerTeamColor() == currentTurn)
            return true;
        else
            return false;
    }
}