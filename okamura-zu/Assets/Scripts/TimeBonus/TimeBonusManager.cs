using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeBonusManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject badge;
    public GameObject updateDataManager;

    //変数宣言
    private TimeBonusData timeBonusData;
    private TimeSpan timeSpan;

    // Start is called before the first frame update
    void Start(){
        InitialSetActive();
        timeBonusData = SaveData.GetClass<TimeBonusData>(SaveDataKeys.timeBonusData,new TimeBonusData());
        timeSpan = DateTime.UtcNow - timeBonusData.GetResetDateTime();
        timeBonusData.DebugInformation();
        updateDataManager.GetComponent<UpdateDataManager>().UpdateTimeBonusData(timeBonusData);
    }

    void InitialSetActive(){
        badge.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
        timeSpan = DateTime.UtcNow - timeBonusData.GetResetDateTime(); //作成された日時と現在の時間の差
        //Debug.Log("span : " + timeSpan.ToString());
        if((int)timeSpan.TotalMinutes > timeBonusData.countUpCount){
            //カウントアップ
            timeBonusData.CountUp();
            SaveData.SetClass<TimeBonusData>(SaveDataKeys.timeBonusData,timeBonusData);
            SaveData.Save();
        };
        updateDataManager.GetComponent<UpdateDataManager>().UpdateTimeBonusData(timeBonusData);
    }

    //集まったコインを取得
    public void GetCoin(){
        //ゲットするりんごの量を決める
        int possessedApple = SaveData.GetInt(SaveDataKeys.possessedPoint);
        possessedApple += timeBonusData.nowNumberOfPossession;
        //データのUI反映
        updateDataManager.GetComponent<UpdateDataManager>().UpdatePossessedPoint(possessedApple);
        
        //timeBonusのリセット
        timeBonusData.Reset();

        //データの保存
        SaveData.SetClass<TimeBonusData>(SaveDataKeys.timeBonusData,timeBonusData);
        SaveData.SetInt(SaveDataKeys.possessedPoint,possessedApple);
        SaveData.Save();
    }
}
