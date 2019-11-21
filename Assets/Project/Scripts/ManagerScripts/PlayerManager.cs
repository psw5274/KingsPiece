using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    public TeamColor playerTeam;
    
    public void Start()
    {
        playerTeam = TeamColor.White;
    }

    public void ChangePlayerTeam()
    {
        playerTeam = (playerTeam == TeamColor.White) ? TeamColor.Black : TeamColor.White;
        CameraManager.Instance.RotateCamera();
    }
    public void SetPlayerTeamColor(TeamColor teamColor)
    {
        playerTeam = teamColor;
        Debug.Log("Player Color" + playerTeam);
    }
}
