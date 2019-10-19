﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AppleManager : MonoBehaviour
{      
    //変数宣言
    public int index; //何番目のりんごか 1~
    private DateTime createDateTime; //作成された日時
    private int remainingTime; //実るまでに必要な時間（秒）
    private bool canHarvest = false; //収穫可能かどうか
    private float finalScale = 0.15f; //最終的なスケール

    void Start(){
        createDateTime = DateTime.UtcNow;
        remainingTime = 30;

        SaveData.GetClass<AppleData>(SaveDataKeys.appleData + index.ToString(),new AppleData());

        //AppleData appleData = new AppleData(DateTime.UtcNow,30);
        
    }

    void Update(){
        TimeSpan timeSpan = DateTime.UtcNow - createDateTime; //作成された日時と現在の時間の差

        if(timeSpan.Seconds >= remainingTime){
            //収穫可能
            canHarvest = true;
        }else{
            //まだ収穫できない
            //スケール変更
            float scale = finalScale * ( (float)timeSpan.Seconds / (float)remainingTime );
            this.GetComponent<RectTransform>().localScale = new Vector3(scale,scale,1);
        }
    }

    //クリックした状態で触れたらタッチ判定
    public void OnPointerEnter(){
        //クリックした状態かつ収穫可能じゃない場合はリターン
        if((Input.GetMouseButton(0) == false) || !canHarvest) return;

        //収穫
        Harvest();
    }

    //クリックしたらタッチ判定
    public void OnPointerClick(){
        //収穫可能じゃなければリターン
        if(!canHarvest) return;

        //収穫
        Harvest();
    }

    private void Harvest(){
        createDateTime = DateTime.UtcNow;
        canHarvest = false;

        Debug.Log("収穫！！");
    }

}
