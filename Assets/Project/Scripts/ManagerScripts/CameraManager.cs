using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    public GameObject playerCamera;
    public Transform whiteCameraTransform;
    public Transform blackCameraTrnasform;
    
    private static CameraManager instance = null;
    public static CameraManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(CameraManager)) as CameraManager;
                if (!instance)
                {
                    Debug.Log("ERROR : NO CameraManager");
                }
            }
            return instance;
        }
    }

    void Start ()
    {
        whiteCameraTransform = playerCamera.transform;
	}
	
	public void RotateCamera()
    {
        playerCamera.transform.Rotate(new Vector3(0, 180, 0), Space.World);
        playerCamera.transform.position = new Vector3(playerCamera.transform.position.x,
                                                      playerCamera.transform.position.y,
                                                      -playerCamera.transform.position.z);

        foreach(Transform child in BoardManager.Instance.pieceZone.transform)
        {
            child.eulerAngles = new Vector3(-child.eulerAngles.x,
                                            child.eulerAngles.y,
                                            child.eulerAngles.z);
            child.localScale = new Vector3(child.localScale.x,
                                           child.localScale.y,
                                           -child.localScale.z);
        }
	}
}
