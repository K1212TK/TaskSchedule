using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ImageManager : MonoBehaviour
{
    public Image image;

    private int taskNo;
    private int closeFlag;
    private DateTime updateTime;

    //プロパティー
    public int TaskNo
    {
        get { return this.taskNo; }  //取得用
        set { this.taskNo = value; } //値入力用
    }

    public int CloseFlag
    {
        get { return this.closeFlag; }  //取得用
        set { this.closeFlag = value; } //値入力用
    }

    public DateTime UpdateTime
    {
        get => updateTime;
        set => updateTime = value;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //背景色の変更
    public void BackGroundChange(Color changeColor)
    {
        //背景色を変更する
        image.color = changeColor;

    }
}
