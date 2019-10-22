using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AppleManager : MonoBehaviour
{      
    //変数宣言
    public int index; //何番目のりんごか 1~
    private bool canHarvest = false; //収穫可能かどうか
    private float finalScale = 0.15f; //最終的なスケール
    private AppleData appleData; //アップルデータ

    void Start(){
        appleData = SaveData.GetClass<AppleData>(SaveDataKeys.appleData + index.ToString(),new AppleData());
        SaveData.SetClass<AppleData>(SaveDataKeys.appleData + index.ToString(),appleData);
        SaveData.Save();
    }

    void Update(){
        TimeSpan timeSpan = DateTime.UtcNow - appleData.GetCreateDateTime(); //作成された日時と現在の時間の差

        if(timeSpan.TotalSeconds >= appleData.remainingTime){
            //収穫可能
            canHarvest = true;
        }else{
            //まだ収穫できない
            //スケール変更
            float scale = finalScale * ( (int)timeSpan.TotalSeconds / (float)appleData.remainingTime );
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
        //新しいりんごの生成
        appleData = new AppleData();
        SaveData.SetClass<AppleData>(SaveDataKeys.appleData + index.ToString(),appleData);
        SaveData.Save();

        canHarvest = false;
    }

}
