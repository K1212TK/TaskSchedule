using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TaskPrefabManager : MonoBehaviour
{
    Task taskData;
    public GameObject content;
    GameObject taskPrefabObj;
    List<GameObject> taskViewList;
    const float X_POS = 0.0f;
    const float Z_POS = 0.0f;
    const float Y_POS = 0.0f;
    const string PREFAB_NAME = "TaskImage";
    //完了チェックのindex
    public const int COMPLETE_CHECK_CHILD_INDEX = 1;
    //削除チェックのindex
    public const int DELETE_CHECK_CHILD_INDEX = 2;

    private const string RESET_TIME = "00:00:00";
    private DateTime today;

    // Start is called before the first frame update
    //起動時のprefabを作成
    void Start()
    {
        //プレハブをGameObject型で取得
        taskPrefabObj = (GameObject)Resources.Load(PREFAB_NAME);
        //全プレハブを格納するlist
        taskViewList = new List<GameObject>();
        //tableのオブジェクト読み込み
        taskData = GetComponent<Task>();
        //リセット用日付設定
        today =  DateTime.Today;
        //プレハブの作成
        CreatePrefab();
    }

    //プレハブを作成する
    private void CreatePrefab()
    {   
        //テーブル内データの全取得
        DataTable  taskTableData = taskData.SelectAll();
        //リストに追加する処理を追加する
        foreach (var row in taskTableData.Rows)
        {
            //取得したデータに置き換える
            GameObject instance = Instantiate(taskPrefabObj, new Vector3(X_POS, Y_POS, Z_POS), Quaternion.identity);
            //各プレハブにタスクnoを設定する
            instance.GetComponent<ImageManager>().TaskNo = (int)row[Task.TASK_NO];
            //クローズフラグを見る
            instance.GetComponent<ImageManager>().CloseFlag = (int)row[Task.CLOSE_FLG];
            //更新時間を設定
            instance.GetComponent<ImageManager>().UpdateTime = DateTime.Parse((string)row[Task.UPDATE_TIME]);
            //dbから取得したデータをはめ込む
            instance.GetComponentInChildren<Text>().GetComponent<TextManager>().ChangeText((string)row[Task.TASK_NAME]);
            //キャンバス上にinstance配置
            instance.transform.SetParent(content.transform, false);
            DateTime today = DateTime.Today;
            int closeFlag = (int)row[Task.CLOSE_FLG];
            //今日よりも前の日だったらリセット
            if (today > instance.GetComponent<ImageManager>().UpdateTime)
            {
                Debug.Log(instance.GetComponent<ImageManager>().UpdateTime);
                //リセット
                taskData.CloseFlagUpdate(instance.GetComponent<ImageManager>().TaskNo, 0);
                closeFlag = 0;
            }
            //トグルのチェックを変更
            instance.GetComponent<ImageManager>().transform.GetChild(1).GetComponent<ToggleManager>().ChangeTogglet(closeFlag);
            //リストへオブジェクトを格納
            taskViewList.Add(instance);
        }
    }

    //prefabの追加を行う
    public void AddPrefab()
    {
        //task_noが一番大きいデータの取得
        DataRow taskTableData = taskData.SelectNewRocord();
        //取得したデータに置き換える
        var instance = Instantiate(taskPrefabObj, new Vector3(X_POS, Y_POS, Z_POS), Quaternion.identity);
        //dbから取得したタスク名に変更する
        instance.GetComponentInChildren<Text>().GetComponent<TextManager>().ChangeText((string)taskTableData[Task.TASK_NAME]); 
        //キャンバス上にinstance配置
        instance.transform.SetParent(content.transform, false);
        //リストへオブジェクトを格納
        taskViewList.Add(instance);
    }

    //全プレハブ文字列の左に余白を開ける
    public void PaddingLeftTextAll()
    {
        foreach (var task in taskViewList)
        {
            if (task.gameObject != null)
            {
                task.GetComponentInChildren<Text>().GetComponent<TextManager>().PaddingLeftText();
            }
        }
    }

    //余白をもとに戻す
    public void CleanPaddingLeftTextAll()
    {
        foreach (var task in taskViewList)
        {
            if (task.gameObject != null)
            {
                task.GetComponentInChildren<Text>().GetComponent<TextManager>().CleanPaddingLeftText();
            }
        }
    }

    //全プレハブの削除選択ボックスを活性にする
    public void ActiveToggleAll(int childIndex)
    {
        foreach (var task in taskViewList)
        {
            if (task.gameObject != null)
            {
                task.transform.GetChild(childIndex).gameObject.SetActive(true);
            }
        }
    }

    //プレハブの完了ボタンを非活性にする
    public void NonActiveToggleAll(int childIndex)
    {
        foreach (var task in taskViewList)
        {
            //nullチェック
            if (task.gameObject != null)
            {
                task.transform.GetChild(childIndex).gameObject.SetActive(false);
            }
        }
    }

    //指定されたプレハブを削除する
    public List<GameObject> CreateDeletePrefabData(int childIndex)
    {
        List<GameObject> deletePrefabList = new List<GameObject>();

        foreach (var task in taskViewList)
        {
            //gameobjectのnullチェック
            if (task.gameObject != null) {
                //フラグの確認
                if (task.transform.GetChild(childIndex).gameObject.GetComponent<UnityEngine.UI.Toggle>().isOn)
                {
                    //削除対象のデータをlistに追加する
                    deletePrefabList.Add(task);
                }
            }
        }
        return deletePrefabList;
    }

    //prefabの削除を実行
    public void DeletePrefab(List<GameObject> deleteDataList)
    {
        foreach (var delData in deleteDataList)
        {
            //sqliteからデータの削除
            taskData.Delete(delData.GetComponent<ImageManager>().TaskNo);
            //prefabの削除
            Destroy(delData.gameObject);
            
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            //ポーズ時に保存する
            foreach (var task in taskViewList)
            {
                if (task.gameObject != null)
                {
                    taskData.CloseFlagUpdate(task.GetComponent<ImageManager>().TaskNo, task.GetComponent<ImageManager>().CloseFlag);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            //ポーズ時に保存する
            foreach (var task in taskViewList)
            {
                if (task.gameObject != null)
                {
                    taskData.DebugCloseFlagUpdate(task.GetComponent<ImageManager>().TaskNo, task.GetComponent<ImageManager>().CloseFlag);
                }
            }
        }

    }

    //background復帰時にも時間リセット処理を入れる
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            //ポーズ時に保存する
            foreach (var task in taskViewList)
            {
                if (task.gameObject != null)
                {
                    //同一の日のみ更新
                    if (today == task.GetComponent<ImageManager>().UpdateTime)
                    {
                        taskData.CloseFlagUpdate(task.GetComponent<ImageManager>().TaskNo, task.GetComponent<ImageManager>().CloseFlag);
                    }
                }
            }
        }
        else {
            if (taskViewList != null)
            {
                foreach (var task in taskViewList)
                {
                    if (task.gameObject != null)
                    {
                        //今日よりも前の日だったらリセット
                        if (today > task.GetComponent<ImageManager>().UpdateTime)
                        {
                            //リセット
                            taskData.CloseFlagUpdate(task.GetComponent<ImageManager>().TaskNo, 0);
                            task.GetComponent<ImageManager>().transform.GetChild(1).GetComponent<ToggleManager>().ChangeTogglet(0);
                        }
                    }
                }
            }
        }
    }
}
