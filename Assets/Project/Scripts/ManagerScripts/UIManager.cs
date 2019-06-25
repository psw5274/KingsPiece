using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// 진짜 이딴식으로 할꺼임? 렬루렬루?
public class UIManager : MonoBehaviour {
    // Singleton
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(UIManager)) as UIManager;
                if (!_instance)
                {
                    Debug.Log("ERROR : NO BOARDMANAGER");
                }
            }
            return _instance;
        }
    }
    // Singleton

    [SerializeField]
    private Text victoryText;

    public void SetVictoryText(int teamColor)
    {
        victoryText.text = (teamColor>0?"WHITE":"BLACK")+" TEAM WIN!!";
        victoryText.enabled = true;
        
    }

	// Use this for initialization
	void Awake ()
    {
        victoryText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
