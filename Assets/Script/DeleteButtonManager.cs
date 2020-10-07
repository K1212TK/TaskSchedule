using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButtonManager : MonoBehaviour
{
    public GameObject taskObj;
    public GameObject addTaskButtonOjb;
    TaskPrefabManager taskPrefabScript;
    public GameObject deleteButtonObj;
    public SelectButtonText buttonText;



    //削除選択ボタンが押された判定
    bool delSelectFlg;

    // Start is called before the first frame update
    void Start()
    {
        taskPrefabScript = taskObj.GetComponent<TaskPrefabManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //削除選択ボタンが押下時処理
    public void OnClickDeleteSelectButton()
    {
        //削除選択を押した場合
        if (!delSelectFlg)
        {

            //すべてのテキストの左に余白を開ける
            taskPrefabScript.PaddingLeftTextAll();
            //削除選択チェックを活性にする
            taskPrefabScript.ActiveToggleAll(TaskPrefabManager.DELETE_CHECK_CHILD_INDEX);
            //完了チェックを非活性にする
            taskPrefabScript.NonActiveToggleAll(TaskPrefabManager.COMPLETE_CHECK_CHILD_INDEX);
            //タスク追加ボタンを非活性に変更する
            addTaskButtonOjb.SetActive(false);
            //削除ボタンを活性にする
            deleteButtonObj.SetActive(true);
            //削除フラグon
            delSelectFlg = true;
            buttonText.ChangeText("解除");
        }
        else
        {
            //余白を削除する
            taskPrefabScript.CleanPaddingLeftTextAll();
            //削除選択チェックを非活性にする
            taskPrefabScript.NonActiveToggleAll(TaskPrefabManager.DELETE_CHECK_CHILD_INDEX);
            //完了チェックを活性にする
            taskPrefabScript.ActiveToggleAll(TaskPrefabManager.COMPLETE_CHECK_CHILD_INDEX);
            //タスク追加ボタンを活性にする
            addTaskButtonOjb.SetActive(true);
            //削除ボタンを非活性にする
            deleteButtonObj.SetActive(false);
            //削除フラグoff
            delSelectFlg = false;
            buttonText.ChangeText("選択");
        }
    }

    //削除ボタンが押された際の処理
    public void OnClickDeleteButton()
    {
        //削除対象データlistの作成
        var deleteDataList = taskPrefabScript.CreateDeletePrefabData(TaskPrefabManager.DELETE_CHECK_CHILD_INDEX);
        //削除実行処理の実行
        taskPrefabScript.DeletePrefab(deleteDataList);

    }
}