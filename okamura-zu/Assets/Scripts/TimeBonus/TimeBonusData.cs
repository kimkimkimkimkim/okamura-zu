using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class TimeBonusData
{
    private DateTime resetDateTime; //リセットされた日時
    public string strResetDateTime; //リセットされた日時(文字列)
    public int productionAmountParMinutes; //1分あたりの生成量
    public int maxNumberOfPossession; //最大所持数
    public int nowNumberOfPossession; //現在の所持数
    public int countUpCount; //カウントアップした回数

    public TimeBonusData(){
        resetDateTime = DateTime.UtcNow;
        strResetDateTime = resetDateTime.ToBinary().ToString();
        productionAmountParMinutes = 1;
        maxNumberOfPossession = 1000;
        nowNumberOfPossession = 0;
        countUpCount = 0;
    }

    //所持数増加させる関数
    public void CountUp(){
        if(nowNumberOfPossession >= maxNumberOfPossession)return; 

        int temp = nowNumberOfPossession + productionAmountParMinutes;

        if(temp <= maxNumberOfPossession){
            //通常のカウントアップ
            countUpCount++;
            nowNumberOfPossession = temp;
        }else{
            //カウントアップしたら最大値超えた
            countUpCount++;
            nowNumberOfPossession = maxNumberOfPossession;
        }
    }

    //プレイヤーが受け取ったらリセット
    public void Reset(){
        nowNumberOfPossession = 0;
        resetDateTime = DateTime.UtcNow;
        strResetDateTime = resetDateTime.ToBinary().ToString();
    }

    //resetDateTime(DateTime型)を返す
    public DateTime GetResetDateTime(){
        return DateTime.FromBinary(Convert.ToInt64(strResetDateTime));
    }

    //パラメーターの出力
    public void DebugInformation(){
        Debug.Log("TimeBonusData");
        Debug.Log("ResetDateTime : " + GetResetDateTime());
        Debug.Log("productionAmountParMinutes : " + productionAmountParMinutes);
        Debug.Log("maxNumberOfPossession : " + maxNumberOfPossession);
        Debug.Log("nowNumberOfPossession : " + nowNumberOfPossession);
        Debug.Log("countUpCount : " + countUpCount);
    }


}
