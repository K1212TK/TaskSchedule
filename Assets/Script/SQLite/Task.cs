using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : TaskSchedule
{
    //テーブル名の宣言
    public const string TABLE_NAME = "task";
    //カラム名
    public const string TASK_NO = "task_no";
    public const string TASK_NAME = "task_name";
    public const string CLOSE_FLG = "close_flg";
    public const string UPDATE_TIME = "update_time";
    public const string CREATE_TIME = "create_time";
    //フラグのデフォルト値
    const int CLOSE_FLG_NO = 0;
    //インサート文のフォーマット
    const string INSERT_FORMAT = "INSERT INTO {0} ({1}, {2}, {3}, {4}) VALUES ('{5}', {6}, '{7}', '{8}')";
    //UPDATE文のフォーマット
    const string UPDATE_CLOSE_FLAG_FORMAT = "UPDATE {0} SET {1} = '{2}',{3} = '{4}' WHERE {5} = {6}";

    // Start is called before the first frame update
    protected override void Start()
    {
    }

    private void Awake()
    {
        //親のstart呼び出し
        base.Start();

    }

    //テーブル内のデータを全件取得
    public DataTable SelectAll()
    {           
        //sqlの作成
        string selectSql = string.Format(SELECT_FORMAT, TABLE_NAME);

        DataTable query = db.ExecuteQuery(selectSql);

        return query;
    }

    //INSERT処理
    public void Insert(string taskName)
    {
        DateTime now = DateTime.Now;
        //insert文の作成
        string insertSql = string.Format(
                INSERT_FORMAT, 
                TABLE_NAME,
                TASK_NAME,
                CLOSE_FLG,
                UPDATE_TIME,
                CREATE_TIME,
                taskName, 
                CLOSE_FLG_NO,
                now,
                now
            );
        //insert実行
        db.ExecuteNonQuery(insertSql);
    }

    //task_noの最大値取得
    public DataRow SelectNewRocord()
    {
        string selectSql = string.Format(SELECT_ORDER_BY, TABLE_NAME, TASK_NO);
        DataTable query = db.ExecuteQuery(selectSql);
        
        return query.Rows[0];
    }

    //削除処理
    public void Delete(int taskNo)
    {
        string deleteTableSql = string.Format(DELETE_FROM,TABLE_NAME);
        string whereSql = string.Format(WHERE, TASK_NO + "=" + taskNo);

        //削除実行
        db.ExecuteNonQuery(deleteTableSql + " " + whereSql);
    }


    //更新処理
    public void CloseFlagUpdate(int taskNo,int closeFlag)
    {
        //update文の更新
        DateTime now = DateTime.Now;
        string updateSql = string.Format(
                UPDATE_CLOSE_FLAG_FORMAT,
                TABLE_NAME,// {0}
                CLOSE_FLG,// {1}
                closeFlag,// {2}
                UPDATE_TIME,// {3}
                now,// {4}
                TASK_NO,// {5}
                taskNo// {6}
            );
        //更新実行
        db.ExecuteNonQuery(updateSql);
    }

    //更新処理 TODO　後で消す
    public void DebugCloseFlagUpdate(int taskNo, int closeFlag)
    {
        //update文の更新
        DateTime now = DateTime.Today.AddDays(-1);
        string updateSql = string.Format(
                UPDATE_CLOSE_FLAG_FORMAT,
                TABLE_NAME,// {0}
                CLOSE_FLG,// {1}
                closeFlag,// {2}
                UPDATE_TIME,// {3}
                now,// {4}
                TASK_NO,// {5}
                taskNo// {6}
            );
        //更新実行
        db.ExecuteNonQuery(updateSql);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
