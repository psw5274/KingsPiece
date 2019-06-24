using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public TeamColor playerTeam;

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

    public void ChangePlayerTeam()
    {
        playerTeam = (playerTeam == TeamColor.White) ? TeamColor.Black : TeamColor.White;
        CameraManager.Instance.RotateCamera();
    }
}
