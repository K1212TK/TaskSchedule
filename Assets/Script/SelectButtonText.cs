using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonText : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //ボタンに表示される文字の置き換え
    public void ChangeText(string buttonText)
    {
        text.text =  buttonText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
