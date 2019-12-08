using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Image statATK_IMG;
    public Text statATK_TXT;

    public Image statHP_IMG;
    public Text statHP_TXT;

    public void SetUIParam(int statATK, int statHP)
    {
        statATK_TXT.text = statATK.ToString();
        statHP_TXT.text = statHP.ToString();
    }

    public void UIEnable(bool enable)
    {
        if(enable)
        {
            statATK_IMG.enabled = true;
            statHP_IMG.enabled = true;
        }
        else
        {
            statATK_IMG.enabled = false;
            statHP_IMG.enabled = false;
        }
    }

    
    public Piece cardData;

    private void Start()
    {
        cardData = GetComponentInParent<Piece>();
    }

    private void Update()
    {
        statATK_TXT.text = cardData.CurrentATK.ToString();
        statHP_TXT.text = cardData.CurrentHP.ToString();
    }
}
