using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {   
    }

    //左に余白を開ける
    public void PaddingLeftText()
    {
        text.transform.Translate(40f, 0f, 0f);
    }

    //左に開けた余白を消す
    public void CleanPaddingLeftText()
    {
        text.transform.Translate(-40f, 0f, 0f);

    }

    //文字列の置き換え
    public void ChangeText(string changeText)
    {
        text.text = changeText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
