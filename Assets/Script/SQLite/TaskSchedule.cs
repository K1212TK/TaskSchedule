using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSchedule : MonoBehaviour
{

    protected const string DB_NAME = "TaskSchedule.db";

    protected const string SELECT_FORMAT = "SELECT * FROM {0}";

    protected const string SELECT_MAX = "SELECT MAX({0}) FROM {1}";

    protected const string SELECT_ORDER_BY = "SELECT * FROM  {0} ORDER BY {1} DESC";

    protected const string DELETE_FROM = "DELETE FROM {0}";

    protected const string WHERE = "WHERE {0}";

    protected SqliteDatabase db;

    // Start is called before the first frame update
   protected virtual void Start()
   {
        //dbの読み込み
        db = new SqliteDatabase(DB_NAME);
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
