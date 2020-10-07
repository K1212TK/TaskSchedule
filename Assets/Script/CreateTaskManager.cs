using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTaskManager : MonoBehaviour
{

    //InputFieldを格納するための変数
    InputField inputField;
    GameObject settingCanvas;
    CanvasManager canvasManagerScript;
    Task task;

    const string INPUT_FIELD_OBJECT_NAME = "TaskAddInputField";
    const string CANVAS_OBJECT_NAME = "SettingCanvas";



    // Start is called before the first frame update
    void Start()
    {
        //InputFieldコンポーネントを取得
        inputField = GameObject.Find(INPUT_FIELD_OBJECT_NAME).GetComponent<InputField>();
        //キャンバスのオブジェクト取得
        settingCanvas = GameObject.Find(CANVAS_OBJECT_NAME);
        //キャンバスに紐付いたスクリプトの取得
        canvasManagerScript = settingCanvas.GetComponent<CanvasManager>();
        //taskスクリプトの取得
        task = GetComponent<Task>();
    }

    //入力値の取得
    public void GetInputText()
    {
        //入力値の登録
        task.Insert(inputField.text);
        //入力値のリセット
        CleanInputText();
        //キャンバスを閉じる
        canvasManagerScript.CloseCanvas();
        //プレハブの追加
        GameObject.Find("TaskObject").GetComponent<TaskPrefabManager>().AddPrefab();
    }


    public void CleanInputText()
    {

        inputField.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
