using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    public Toggle toggle;
    public ImageManager image;
    //public GameObject imageObj;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //現在のtogleの設定値を取得する。
    public void ToggleCheck()
    {
        if (toggle.isOn)
        {
            //背景色を変更する
            image.BackGroundChange(Color.gray);
            image.CloseFlag = 1;
            image.UpdateTime = DateTime.Now;
        }
        else
        {
            image.BackGroundChange(Color.white);
            image.CloseFlag = 0;
        }
    }

    //文字列の置き換え
    public void ChangeTogglet(int closeFlag)
    {
        toggle.isOn = Convert.ToBoolean(closeFlag);
    }
}
