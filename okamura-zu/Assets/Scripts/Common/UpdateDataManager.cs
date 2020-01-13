using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateDataManager : MonoBehaviour
{
    //オブジェクト参照
    public GameObject txtPossessedPointInGacha; //ガチャ画面の所持ポイントテキスト
    public GameObject allyAreaManager;
    public GameObject mainSceneManager;
    /*timeBonus*/
    public GameObject badge;
    public GameObject textProductionAmountParMinutes;
    public GameObject textPlayTime;
    public GameObject textMaxNumberOfPossession;
    public GameObject textNowNumberOfPossession;
    public GameObject textNowNumberOfPossession2;

    public void UpdatePossessedPoint(int point){
        txtPossessedPointInGacha.GetComponent<Text>().text = point.ToString();
    }

    public void UpdatePossessedNamakemonoList(List<NamakemonoData> ndList){
        allyAreaManager.GetComponent<AllyAreaManager>().SetAlly(ndList);
    }

    public void UpdatePlayer(PlayerData player){
        mainSceneManager.GetComponent<MainSceneManager>().UpdatePlayer(player);
    }

    public void UpdateTimeBonusData(TimeBonusData timeBonusData){
        TimeSpan timeSpan = DateTime.UtcNow - timeBonusData.GetResetDateTime();
        badge.SetActive(timeBonusData.nowNumberOfPossession == timeBonusData.maxNumberOfPossession);
        textProductionAmountParMinutes.GetComponent<Text>().text = "+" + timeBonusData.productionAmountParMinutes + "/分";
        textPlayTime.GetComponent<Text>().text = "総プレイ時間 " + ZeroPadding((int)timeSpan.TotalHours) + ":" + ZeroPadding((int)timeSpan.Minutes) + ":" + ZeroPadding((int)timeSpan.Seconds);
        textMaxNumberOfPossession.GetComponent<Text>().text = "/"+timeBonusData.maxNumberOfPossession;
        textNowNumberOfPossession.GetComponent<Text>().text = timeBonusData.nowNumberOfPossession.ToString();
        textNowNumberOfPossession2.GetComponent<Text>().text = "×" + timeBonusData.nowNumberOfPossession;
    }

    string ZeroPadding(int num){
        string str = "0" + num;
        return str.Substring(str.Length-2);
    }
}
