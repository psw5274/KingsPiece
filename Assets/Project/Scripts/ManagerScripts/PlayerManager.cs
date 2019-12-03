using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Manager<PlayerManager>
{
    [SerializeField]
    private TeamColor playerTeam;// = TeamColor.White;


    public void SetPlayerTeamColor(TeamColor teamColor)
    {
        playerTeam = teamColor;
        Debug.Log("Set Player Team Color" + playerTeam);
    }

    public TeamColor GetPlayerTeamColor()
    {
        return playerTeam;
    }
}
