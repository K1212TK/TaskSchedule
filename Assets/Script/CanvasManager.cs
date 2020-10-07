using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        if (canvas != null)
        {
            canvas.enabled = false;
        }
    }

    //キャンバスを開く
    public void OpenCanvas()
    {
        canvas.enabled = true;
    }

    //キャンバスを閉じる
    public void CloseCanvas()
    {
        canvas.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
