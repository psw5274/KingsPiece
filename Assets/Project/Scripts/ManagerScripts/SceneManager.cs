using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// !!!임시!!!
public class SceneManager : MonoBehaviour
{
    public static Queue<string> nextScene = new Queue<string>();

    private static SceneManager instance = null;
    public static SceneManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(SceneManager)) as SceneManager;
                if (!instance)
                {
                    Debug.Log("ERROR : NO SceneManager");
                }
            }
            return instance;
        }
    }

    public static void NewScene(string sceneName)
    {
        nextScene.Enqueue(sceneName);
    }

    private void StartScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Project/Scenes/Playing");

    }

    void Update()
    {
        if(nextScene.Count > 0)
        {
            StartScene(nextScene.Dequeue());
        }
    }
}
