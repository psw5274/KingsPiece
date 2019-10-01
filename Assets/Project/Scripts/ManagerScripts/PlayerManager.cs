using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public static TeamColor playerTeam;

    private static PlayerManager instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(PlayerManager)) as PlayerManager;
                if (!instance)
                {
                    Debug.Log("ERROR : NO PlayerManager");
                }
            }
            return instance;
        }
    }

    public void Start()
    {
        playerTeam = TeamColor.White;
    }

    public static void SetPlayerTeamColor(TeamColor teamColor)
    {
        playerTeam = teamColor;
        Debug.Log("Player Color" + playerTeam);
    }

    /* No more use
     * 
    public void ChangePlayerTeam()
    {
        playerTeam = (playerTeam == TeamColor.White) ? TeamColor.Black : TeamColor.White;
        CameraManager.Instance.RotateCamera();
    }
    */
}
