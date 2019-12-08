using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public Text victoryText;
    public Button endTurnButton;


    public void SetVictoryText(int teamColor)
    {
        victoryText.text = (teamColor>0?"WHITE":"BLACK")+" TEAM WIN!!";
        victoryText.enabled = true;
        
    }

    public void OnClickEndTurnButton()
    {
        if(GameManager.Instance.IsPlayerTurn())
        {
            GameManager.Instance.NewTurn();
        }
    }

    public void SwitchEndTurnButtonText()
    {
        if (GameManager.Instance.IsPlayerTurn())
        {
            endTurnButton.GetComponentInChildren<Text>().text = "턴 종료";
            endTurnButton.enabled = true;

            endTurnButton.image.color = new Color(255, 255, 255);
        }
        else
        {
            endTurnButton.GetComponentInChildren<Text>().text = "상대 턴";
            endTurnButton.enabled = false;

            endTurnButton.image.color = new Color(195, 195, 195);
        }
    }
}
